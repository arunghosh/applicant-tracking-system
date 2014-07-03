using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rdt.CourseFinder.Controllers
{
    public class PassReturnController : BaseController
    {

        public PartialViewResult PendingAdmin()
        {
            var candidates = _db.Candidates.Where(c => c.PassportReturnStatus == PassportReturnStatus.PendingAdminApproval).ToList();
            var model = new PassReturnVm
            {
                Candidates = candidates,
                AgreeStatus = (byte)PassportReturnStatus.PendingAccountApproval,
                RejectStatus = (byte)PassportReturnStatus.AdminRejected
            };
            return PartialView("List", model);
        }

        public PartialViewResult PendingAccounts()
        {
            var candidates = _db.Candidates.Where(c => c.PassportReturnStatus == PassportReturnStatus.PendingAccountApproval).ToList();
            var model = new PassReturnVm
            {
                Candidates = candidates,
                AgreeStatus = (byte)PassportReturnStatus.Approved,
                RejectStatus = (byte)PassportReturnStatus.AccountsRejected
            };
            return PartialView("List", model);
        }

        //public PartialViewResult PendingAdmin()
        //{
        //    var candidates = _db.Candidates.Where(c => c.PassportReturnStatus == PassportReturnStatus.PendingAccountApproval).ToList();
        //    return PartialView(candidates);
        //}

        [HttpPost]
        public JsonResult UpdateStatus(int id, byte sid, string msg)
        {
            var status = true;
            var logTxt = string.Empty;
            var newStaus = (PassportReturnStatus)sid;
            try
            {

                var candidate = _db.Candidates.Find(id);
                candidate.PassportReturnStatus = newStaus;
                switch (newStaus)
                {
                    case PassportReturnStatus.NotRequested:
                        logTxt = "Cancelled Passport Return Request";
                        break;
                    case PassportReturnStatus.PendingAdminApproval:
                        candidate.PassportReturnReason = msg;
                        logTxt = "Requested passport return. Pending Admin approval";
                        break;
                    case PassportReturnStatus.PendingAccountApproval:
                        logTxt = "Passport return approved by Admin. Pending Accounts approval";
                        break;
                    case PassportReturnStatus.PassportReturned:
                        logTxt = "Passport returned";
                        break;
                    case PassportReturnStatus.AdminRejected:
                        logTxt = "Passport return rejected by Admin";
                        break;
                    case PassportReturnStatus.AccountsRejected:
                        logTxt = "Passport return rejected by Accounts";
                        break;
                    case PassportReturnStatus.Approved:
                        logTxt = "Passport return approved";
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
            return Json(new { status = status, msg = logTxt });
        }
    }
}
