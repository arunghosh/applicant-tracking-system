using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Models;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Controllers
{
    public class NotificationController : BaseController
    {
        //
        // GET: /Notification/

        public PartialViewResult Index()
        {
            using (var srv = new NotifyService())
            {
                return PartialView(srv.GetNotifications());
            }
        }

        public PartialViewResult FromDB()
        {
            var userId = CurrentUserId;
            var ntfys = _db.Notifications.Where(n => n.UserId == userId && !n.IsRead).ToList();
            return PartialView(ntfys);
        }

        public PartialViewResult Details()
        {
            using (var srv = new NotifyService())
            {
                return PartialView(srv.GetNotifications());
            }
        }

        public PartialViewResult VisaDeposit(int days = 3)
        {
            var today = DateTime.Now.Date;
            var visaNotifyDate = today.AddDays(days).Date;
            var candidates = _db.Candidates
                                .Where(c => !c.IsVisaDeposited).ToList()
                                .Where(c => c.TravelDate != null && c.TravelDate.Value <= visaNotifyDate && c.TravelDate >= today)
                                .ToList();
            return PartialView(candidates);            
        }
    }
}
