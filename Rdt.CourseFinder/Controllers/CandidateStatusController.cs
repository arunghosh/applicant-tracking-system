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
            return View(_db.CandidateStatuses.ToList());
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
