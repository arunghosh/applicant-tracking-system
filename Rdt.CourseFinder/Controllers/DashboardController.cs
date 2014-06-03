using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Rdt.CourseFinder.Controllers
{
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            CurrentPage = PageTypes.Dashboard;
            var prjs = _db.Projects.Include(p => p.Candidates).ToList();
            return View();
        }

        public PartialViewResult Projects()
        {
            var prjs = _db.Projects.Include(p => p.Candidates).ToList();
            return PartialView(CurrentUser.Role.ToString(), prjs);
        }
    }
}
