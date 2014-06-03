using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Controllers
{
    public class RequirementController : BaseController
    {
        [HttpGet]
        public PartialViewResult Edit(int? id, int prjId)
        {
            var model = id == null
                ? new Requirement
                {
                    ProjectId = prjId
                }
                : _db.Requirements.Find(id);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Requirement model)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(model).State = model.IsNew
                                            ? EntityState.Added
                                            : EntityState.Modified;
                model.Category = model.Category.Trim();
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Remove(Requirement model)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(model).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }

        [HttpGet]
        public PartialViewResult List(int id)
        {
            var model = _db.Requirements.Where(r => r.ProjectId == id).ToList();
            return PartialView(model);
        }
    }
}
