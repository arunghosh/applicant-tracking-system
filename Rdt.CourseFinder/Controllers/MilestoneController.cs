using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Controllers
{
    public class MilestoneController : BaseController
    {
        [HttpGet]
        public PartialViewResult Edit(int? id, int prjId)
        {
            var model = id == null
                ? new Milestone
                {
                    ProjectId = prjId,
                    ExpectedDate = DateTime.UtcNow
                }
                : _db.Milestones.Find(id);
            var statusLst = DbCache.Instance.PostCanditStatus;
            ViewBag.CandidateStatusId = new SelectList(statusLst, "CandidateStatusId", "Name", model.CandidateStatusId);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Milestone model)
        {
            if (model.MilestoneType == MilestoneTypes.Status)
            {
                model.Name = DbCache.Instance.CanditStatus.First(m => m.CandidateStatusId == model.CandidateStatusId).Name;
            }
            _db.Entry(model).State = model.IsNew
                                        ? EntityState.Added
                                        : EntityState.Modified;
            _db.SaveChanges();
            return GetErrorMsgJSON();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult Remove(Milestone model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Entry(model).State = EntityState.Deleted;
        //        _db.SaveChanges();
        //    }
        //    return GetErrorMsgJSON();
        //}

        [HttpGet]
        public PartialViewResult Graphical(int id)
        {
            var milestones = _db.Milestones.Where(m => m.ProjectId == id).ToList();
            milestones = milestones.OrderBy(o => o.ExpectedDate).ToList();
            return PartialView(milestones);
        }

        [HttpGet]
        public PartialViewResult List(int id)
        {
            var model = _db.Milestones.Where(r => r.ProjectId == id).ToList();
            return PartialView(model);
        }

    }
}
