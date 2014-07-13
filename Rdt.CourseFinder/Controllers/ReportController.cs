using Rdt.CourseFinder.Models;
using Rdt.CourseFinder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rdt.CourseFinder.Controllers
{
    public class ReportController : BaseController
    {

        [HttpGet]
        public ViewResult Index()
        {
            CurrentPage = PageTypes.Report;
            var model = new ReportVm();
            model.RefreshCandidates();
            return View(model);
        }

        [HttpPost]
        public ViewResult Index(ReportVm model)
        {
            CurrentPage = PageTypes.Report;
            model.RefreshCandidates();
            return View(model);
        }
    }
}
