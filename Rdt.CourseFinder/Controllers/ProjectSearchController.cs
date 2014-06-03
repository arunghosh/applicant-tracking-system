using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Models;

namespace Rdt.CourseFinder.Controllers
{
    public class ProjectSearchController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int? id)
        {
            var model = new ProjectSearchVm(id);
            model.ApplyFilters();
            model.FilteredItems = new List<Candidate>();
            return View(model);
        }

        [HttpPost]
        public ViewResult Index(ProjectSearchVm model)
        {
            model.FillMasterList();
            model.ApplyFilters(Request.Form);
            return View(model);
        }

    }
}
