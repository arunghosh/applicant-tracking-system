using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Controllers
{
    public class CandidateStatusController : BaseController
    {
        //
        // GET: /CandidateStatus/

        public ActionResult Index()
        {
            CurrentPage = PageTypes.Setting;
            return View(_db.CandidateStatuses.ToList().OrderBy(c => c.Index).ToList());
        }


        [HttpPost]
        public JsonResult UpdateIndex(int id, int value)
        {
            var status = true;
            try
            {
                var statusList = _db.CandidateStatuses.ToList();
                var canStat = statusList.Single(c => c.CandidateStatusId == id);
                var oldIndex = canStat.Index;
                var newIndex = oldIndex + value;
                var swapCanStat = statusList.FirstOrDefault(c => c.Index == newIndex);

                if (swapCanStat != null)
                {
                    swapCanStat.Index = oldIndex;
                    _db.Entry(swapCanStat).State = System.Data.EntityState.Modified;
                }
                canStat.Index = newIndex;
                _db.Entry(canStat).State = System.Data.EntityState.Modified;
                _db.SaveChanges();
            }
            catch
            {
                status = false;
            }
            return Json(status);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var model = id == null
                    ? new CandidateStatus()
                    : _db.CandidateStatuses.Find(id);
            return PartialView(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Edit(CandidateStatus model)
        {
            if (ModelState.IsValid)
            {
                var user = CurrentUser;
                var userLog = new UserLog
                {
                    ByUserId = user.UserId,
                    ByUserName = user.Name,
                    UserId = user.UserId
                };

                if (model.IsNew)
                {
                    _db.CandidateStatuses.Add(model);
                    userLog.Comment = "Added new candidate status - " + model.Name;
                }
                else
                {
                    _db.Entry(model).State = System.Data.EntityState.Modified;
                    userLog.Comment = "Updated candidate status - " + model.Name;
                }
                _db.UserLogs.Add(userLog);
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }

    }
}
