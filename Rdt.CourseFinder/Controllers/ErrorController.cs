using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rdt.CourseFinder.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

    }
}
