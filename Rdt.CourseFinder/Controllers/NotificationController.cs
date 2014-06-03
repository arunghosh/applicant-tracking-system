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

        public PartialViewResult Details()
        {
            using (var srv = new NotifyService())
            {
                return PartialView(srv.GetNotifications());
            }
        }
    }
}
