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
        CalenderSrv _srv = new CalenderSrv();

        [HttpGet]
        public ViewResult Index()
        {
            CurrentPage = PageTypes.TravelCalendar;
            var model = new CalenderVm();
            model.Result = _srv.GetTravelling(model.StartDate, model.EndDate);
            return View(model);
        }


        [HttpPost]
        public ViewResult Index(CalenderVm model)
        {
            model.Result = _srv.GetTravelling(model.StartDate, model.EndDate);
            return View(model);
        }

        public PartialViewResult Details(DateTime date)
        {
            var result = _srv.GetTravelling(date);
            return PartialView(result);
        }

    }
}
