using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Models;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Controllers
{
    public class CandidateController : BaseController
    {

        [HttpPost]
        public JsonResult UpdateTravel(TravelDetailsVm model, string idsStr)
        {
            try
            {
                var user = CurrentUser;
                var ids = GetIds(idsStr);
                var candidates = _db.Candidates.Where(c => ids.Contains(c.CandidateId)).ToList();
                foreach (var item in candidates)
                {
                    if (item.TravelDate != null && item.TravelPostponement != TravelPostStates.Approved)
                    {
                        throw new SimpleException("Request travel postponement for " + item.Name);
                    }
                    item.TravelDate = model.TravelDate;
                    item.BoardingCity = model.BoardingCity;
                    item.TravelPostponement = TravelPostStates.NotRequested;
                    item.Airlines = model.Airlines;
                    _db.Entry(item).State = System.Data.EntityState.Modified;
                    var cLog = item.CreateLog("Updated Travel Details", user);
                    _db.CandidateLogs.Add(cLog);
                }
                var uLog = user.CreateLog("Updated travel details of ( " + string.Join(", ", candidates.Select(c => c.Passport)) + " )");
                _db.UserLogs.Add(uLog);
                _db.SaveChanges();
            }
            catch (SimpleException ex)
            {
                AddModelError(ex);
            }
            return GetErrorMsgJSON();
        }

        [HttpPost]
        public JsonResult UpdateMedical(MedicalDetailsVm model, string idsStr)
        {
            var user = CurrentUser;
            var ids = GetIds(idsStr);
            var candidates = _db.Candidates.Where(c => ids.Contains(c.CandidateId)).ToList();
            foreach (var item in candidates)
            {
                item.MedicalDoneDate = model.DoneDate;
                item.MedicalExpiryDate = model.ExpiryDate;
                _db.Entry(item).State = System.Data.EntityState.Modified;
                var cLog = item.CreateLog("Updated Medical Details", user);
                _db.CandidateLogs.Add(cLog);

            }
            var uLog = user.CreateLog("Updated medical details of ( " + string.Join(", ", candidates.Select(c => c.Passport)) + " )");
            _db.UserLogs.Add(uLog);
            _db.SaveChanges();
            return GetErrorMsgJSON();
        }


        [HttpPost]
        public JsonResult UpdateVisa(MedicalDetailsVm model, string idsStr)
        {
            var ids = GetIds(idsStr);
            var user = CurrentUser;
            var candidates = _db.Candidates.Where(c => ids.Contains(c.CandidateId)).ToList();
            foreach (var item in candidates)
            {
                item.VisaIssuesDate = model.DoneDate;
                item.VisaExpiryDate = model.ExpiryDate;
                _db.Entry(item).State = System.Data.EntityState.Modified;

                var cLog = item.CreateLog("Updated Visa Details", user);
                _db.CandidateLogs.Add(cLog);

            }
            var uLog = user.CreateLog("Updated Visa details of ( " + string.Join(", ", candidates.Select(c => c.Passport)) + " )");
            _db.UserLogs.Add(uLog);

            _db.SaveChanges();
            return GetErrorMsgJSON();
        }

        [HttpPost]
        public JsonResult SetVisaDeposit(int id)
        {
            var status = true;
            try
            {
                var user = CurrentUser;
                var candidate = _db.Candidates.Find(id);
                candidate.IsVisaDeposited = true;
                _db.Entry(candidate).State = System.Data.EntityState.Modified;

                var cLog = candidate.CreateLog("Updated Visa Deposit", user);
                _db.CandidateLogs.Add(cLog);

                var uLog = user.CreateLog("Updated Visa deposit of " + candidate.Passport);
                _db.UserLogs.Add(uLog);

                _db.SaveChanges();
            }
            catch
            {
                status = false;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        private List<int> GetIds(string idsStr)
        {
            var ids = new List<int>();
            idsStr.Split(',').ToList().ForEach(c => ids.Add(int.Parse(c)));
            return ids;
        }

        public PartialViewResult Summary(int id)
        {
            var candidates = _db.Candidates.Where(p => p.ProjectId == id).ToList();
            var model = new CandidateSummaryVm(candidates);
            return PartialView(model);
        }

        public PartialViewResult Details(int id)
        {
            var candidate = _db.Candidates
                                    .Include(x => x.Project)
                                    .Include(x => x.CandidateLogs)
                                    .Single(c => c.CandidateId == id);
            return PartialView(candidate);
        }

        [HttpPost]
        public JsonResult UpdateEcCheck(List<int> ids, bool? isReq, bool? isDone)
        {
            var user = CurrentUser;
            var candidates = _db.Candidates.Where(c => ids.Contains(c.CandidateId)).ToList();
            try
            {
                var logTxt = string.Empty;
                if (isReq != null)
                {
                    logTxt += "Required : " + (isReq ?? false);
                }
                if (isDone != null)
                {
                    logTxt += "Done : " + (isDone ?? false);
                }

                foreach (var item in candidates)
                {
                    item.UpdateEcCheck(isReq, isDone);
                    var log = item.CreateLog(string.Format("EC Check Updated. {0}", logTxt), user);
                    _db.Entry(item).State = System.Data.EntityState.Modified;
                    _db.CandidateLogs.Add(log);
                }
                var activityLog = user.CreateLog("EC Check updated of ( " + string.Join(", ", candidates.Select(c => c.Passport)) + " ) : " + logTxt);
                _db.UserLogs.Add(activityLog);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                //AddUserLog(ex.Message);
                return Json(new
                    {
                        msg = ex.Message,
                        status = false
                    });
            }
            return Json(new
            {
                status = true,
                msg = candidates.Select(c => c.EcState)
            }); ;
        }

        [HttpPost]
        public JsonResult UpdateStatus(List<int> ids, int status)
        {
            var updateStatus = true;
            var msg = string.Empty;
            try
            {
                var candidates = _db.Candidates.Where(c => ids.Contains(c.CandidateId)).ToList();
                var user = CurrentUser;
                var canStatus = _db.CandidateStatuses.Find(status);
                foreach (var item in candidates)
                {
                    if (item.CandidateStatusId != status)
                    {
                        item.CandidateStatusId = status;
                        item.ValidateTravelStatus();
                        item.StatusUpdatedAt = DateTime.UtcNow;
                        var log = item.CreateLog(string.Format("Status Updated to '{0}' ##{1}", canStatus.Name, status), user);
                        _db.Entry(item).State = System.Data.EntityState.Modified;
                        _db.CandidateLogs.Add(log);
                    }
                }
                var activityLog = user.CreateLog("Updated status of ( " + string.Join(", ", candidates.Select(c => c.Passport)) + " ) to #" + canStatus.Name);
                _db.UserLogs.Add(activityLog);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                updateStatus = false;
                msg = ex.Message;
            }
            return Json(new
            {
                msg = msg,
                status = updateStatus
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Cancel(string reason, int id)
        {
            var user = CurrentUser;
            var candidate = _db.Candidates.Find(id);
            candidate.CandidateStatusId = Constants.SID_Cancelled;
            _db.Entry(candidate).State = System.Data.EntityState.Modified;
            var logTxt = "Cancelled #" + candidate.Passport + " - " + reason;

            var userLog = user.CreateLog("Cancelled candidate #" + candidate.Passport + " - " + reason);
            _db.UserLogs.Add(userLog);

            var candidateLog = candidate.CreateLog(string.Format("Status Updated to 'Cancelled'. {0}. ##{1}", reason, Constants.SID_Cancelled), user);
            _db.CandidateLogs.Add(candidateLog);

            _db.SaveChanges();
            return Json(true);
        }


        [HttpGet]
        public ViewResult Edit(int? id)
        {
            var model = id == null
                            ? new Candidate()
                            : _db.Candidates.Find(id);
            var projects = _db.Projects.ToList();
            var status = DbCache.Instance.CanditStatus;
            if (model.IsNew)
            {
                model.ProjectId = projects[0].ProjectId;
                model.CandidateStatusId = status[0].CandidateStatusId;
            }
            var ctgrys = _db.Requirements.Where(r => r.ProjectId == model.ProjectId).Select(c => c.Category).ToList();
            ViewBag.ProjectId = new SelectList(projects, "ProjectId", "ProjectName", model.ProjectId);
            ViewBag.CandidateStatusId = new SelectList(status, "CandidateStatusId", "Name", model.CandidateStatusId);
            ViewBag.Category = new SelectList(ctgrys);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Candidate model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = CurrentUser;
                    if (model.IsNew)
                    {
                        if (_db.Candidates.Any(c => c.Passport == model.Passport))
                        {
                            throw new SimpleException("Failed to update. Another candidate with this passport number exists in this project.");
                        }
                        _db.Candidates.Add(model);
                    }
                    else
                    {
                        _db.Entry(model).State = System.Data.EntityState.Modified;
                    }
                    var uLog = user.CreateLog(LogHelper.CanidateUpdate(model));
                    _db.UserLogs.Add(uLog);
                    var cLog = model.CreateLog(model.ChangeStatus, user);
                    _db.CandidateLogs.Add(cLog);
                    _db.SaveChanges();
                }
                catch (SimpleException ex)
                {
                    AddModelError(ex);
                }
            }
            return GetErrorMsgJSON();
        }
    }
}
