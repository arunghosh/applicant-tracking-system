using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Controllers
{
    public class ClientController : BaseController
    {
        public ViewResult Index()
        {
            CurrentPage = PageTypes.Company;
            return View(_db.Companies.ToList());
        }

        public PartialViewResult Edit(int? id)
        {
            var model = id == null
                            ? new Company()
                            : _db.Companies.Find(id);
            return PartialView(model);
        }

        public ViewResult Details(int id)
        {
            var model = _db.Companies.Include(c => c.Projects).Single(c => c.CompanyId == id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Company model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsNew)
                {
                    _db.Companies.Add(model);
                }
                else
                {
                    _db.Entry(model).State = System.Data.EntityState.Modified;
                }
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }
    }
}
