using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Controllers
{
    public class CategoryController : BaseController
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            var categories = _db.MasterCategories.Include(x => x.Categories).ToList();
            return View(categories);
        }

        [HttpGet]
        public PartialViewResult EditMaster(int? id)
        {
            var model = id == null
                        ? new MasterCategory()
                        : _db.MasterCategories.Find(id);
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditMaster(MasterCategory model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsNew)
                {
                    _db.MasterCategories.Add(model);
                }
                else
                {
                    _db.Entry(model).State = System.Data.EntityState.Modified;
                }
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }

        [HttpGet]
        public PartialViewResult EditCtgry(int? id, int pid)
        {
            var model = id == null
                        ? new Category
                        {
                            MasterCategoryId = pid
                        }
                        : _db.Categories.Find(id);
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditCtgry(Category model)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(model);
            }
            else
            {
                _db.Entry(model).State = System.Data.EntityState.Modified;
            }
            _db.SaveChanges();
            return GetErrorMsgJSON();
        }

    }
}
