using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Models;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Controllers
{
    public class CalendarController : BaseController
    {
        ReportSrv _srv = new ReportSrv();

        [HttpGet]
        public ViewResult Index()
        {
            CurrentPage = PageTypes.TravelCalendar;
            var model = new CalenderVm();
            model.Candidates = _srv.GetTravelling(model.StartDate, model.EndDate);
            return View(model);
        }

        [HttpPost]
        public ViewResult Index(CalenderVm model)
        {
            model.Candidates = _srv.GetTravelling(model.StartDate, model.EndDate);
            return View(model);
        }

        public PartialViewResult Details(DateTime date)
        {
            using (var srv = new ReportSrv())
            {
                var result = srv.GetTravelling(date);
                return PartialView(result);
            }
        }

    }
}
