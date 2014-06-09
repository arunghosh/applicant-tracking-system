using Rdt.CourseFinder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rdt.CourseFinder.Controllers
{
    public class TravelPostController : BaseController
    {
        //
        // GET: /TravelPost/

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public JsonResult RequestPost(int id, int sid)
        //{
        //    var status = true;
        //    try
        //    {

        //        var candidate = _db.Candidates.Find(id);
        //        candidate.TravelPostponement = Entities.TravelPostStates.Requested;
        //        _db.Entry(candidate).State = System.Data.EntityState.Modified;

        //        var user = CurrentUser;
        //        var cLog = candidate.CreateLog("Requested Travel Postponement", user);
        //        _db.CandidateLogs.Add(cLog);

        //        var uLog = user.CreateLog("Requested Travel Postponement of " + candidate.Passport);
        //        _db.UserLogs.Add(uLog);

        //        _db.SaveChanges();
        //    }
        //    catch
        //    {
        //        status = false;
        //    }
        //    return Json(status);
        //}

        public PartialViewResult PendingApproval()
        {
            var candidates = _db.Candidates.Where(c => c.TravelPostponement == TravelPostStates.Requested).ToList();
            return PartialView(candidates);
        }

        [HttpPost]
        public JsonResult UpdateStatus(int id, byte sid)
        {
            var status = true;
            var newStaus = (TravelPostStates)sid;
            try
            {

                var candidate = _db.Candidates.Find(id);
                
                var logTxt = string.Empty;
                candidate.TravelPostponement = newStaus;
                switch (newStaus)
                {
                    case TravelPostStates.NotRequested:
                        logTxt = "Cancelled Travel Postponement Request";
                        break;
                    case TravelPostStates.Requested:
                        logTxt = "Requested Travel Postponement";
                        break;
                    case TravelPostStates.Approved:
                        logTxt = "Approved Travel Postponement";
                        break;
                    case TravelPostStates.Rejected:
                        logTxt = "Rejected Travel Postponement";
                        break;
                    default:
                        break;
                }

                var user = CurrentUser;
                var cLog = candidate.CreateLog(logTxt, user);
                _db.CandidateLogs.Add(cLog);

                var uLog = user.CreateLog(logTxt + " of " + candidate.Passport);
                _db.UserLogs.Add(uLog);

                _db.Entry(candidate).State = System.Data.EntityState.Modified;
                _db.SaveChanges();
            }
            catch
            {
                status = false;
            }
            return Json(new { status = status, msg = newStaus.ToString() });
        }

    }
}
