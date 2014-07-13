using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;
using System.Data;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using System.Drawing;
using Rdt.CourseFinder.Models;

namespace Rdt.CourseFinder.Controllers
{
    public class ProjectController : BaseController
    {

        [HttpGet]
        public ActionResult EditMode(int id)
        {
            var project = _db.Projects
                                .Include(x => x.ProjetLogs)
                                .Include(x => x.Company)
                                .Include(x => x.Imports)
                                .Single(x => x.ProjectId == id);
            return View(project);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var project = _db.Projects
                                .Include(x => x.ProjetLogs)
                                .Include(x => x.Company)
                                .Include(x => x.Requirements)
                                .Single(x => x.ProjectId == id);
            return View(project);
        }

        [HttpGet]
        public ActionResult Index()
        {
            CurrentPage = PageTypes.Projects;
            var projects = _db.Projects.ToList();
            return View(projects);
        }


        public PartialViewResult StatusBar(int id, string ctgry)
        {
            var model = new Dictionary<string, Dictionary<string, int>>();
            var prj = _db.Projects.Find(id);
            var summary = new Dictionary<string, int>();
            summary.Add("Requirement", prj.TotalRequirement);
            summary.Add("Recruited", prj.Candidates.Count());
            summary.Add("Medical Received", prj.MedicalReceivedCandidates.Count);
            summary.Add("Travelled", prj.TravelledCandidates.Count);
            model.Add("Summary", summary);
            foreach (var item in prj.CategorySelectList.Skip(1))
            {
                var temp = new Dictionary<string, int>();
                var ctrgyCandits = prj.GetByCategory(item);
                temp.Add("Requirement", prj.RequirementFor(item));
                temp.Add("Recruited", ctrgyCandits.Count());
                temp.Add("Medical Received", ctrgyCandits.Count(m => m.IsMedicalReceived));
                temp.Add("Travelled", ctrgyCandits.Count(m => m.IsStatusComplete(Constants.SID_Travelled)));
                model.Add(item, temp);
            }
            ViewBag.Prj = prj.ProjectName;
            return PartialView(model);
        }


        public PartialViewResult CategoryBar(int id, string ctgry)
        {
            var project = _db.Projects
                    .Include(x => x.ProjetLogs)
                    .Include(x => x.Company)
                    .Include(x => x.Requirements)
                    .Single(x => x.ProjectId == id);
            var candidates = (string.IsNullOrEmpty(ctgry) || ctgry.Contains("["))
                    ? project.Candidates.ToList()
                    : project.GetByCategory(ctgry);
            CandidateSummaryVm model = new CandidateSummaryVm(candidates);
            var statDict = model.Status.ToDictionary(c => c.Key);
            var result = new Dictionary<string, int>();
            var total = model.Status.Sum(s => s.Count());
            if (string.IsNullOrEmpty(ctgry) || ctgry.Contains("["))
            {
                result.Add("Requirement", project.TotalRequirement);
            }
            else
            {
                result.Add("Requirement", project.RequirementFor(ctgry));
            }
            result.Add("Recruited", candidates.Count());

            foreach (var item in DbCache.Instance.CanditStatus)
            {
                if (statDict.ContainsKey(item.Abbrevation))
                {
                    result.Add(item.Name, statDict[item.Abbrevation].Count());
                }
                else
                {
                    result.Add(item.Name, 0);
                }
            }
            ViewBag.Ctrgy = ctgry;
            ViewBag.Prj = project.ProjectName;
            return PartialView(result);
        }

        public PartialViewResult StatusPie(int id, string ctgry)
        {
            var candidates = (string.IsNullOrEmpty(ctgry) || ctgry.Contains("["))
                    ? _db.Candidates.Where(p => p.ProjectId == id).ToList()
                    : _db.Candidates.Where(p => p.ProjectId == id && ctgry == p.Category).ToList();
            var model = new CandidateSummaryVm(candidates);
            var result = new Dictionary<string, int>();
            var total = model.Status.Sum(s => s.Count());
            foreach (var item in model.Status)
            {
                result.Add(item.Key, item.Count());
            }
            return PartialView(result);
        }


        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var model = id == null
                ? new Project()
                : _db.Projects.Find(id);
            var companies = _db.Companies.ToList();
            if (model.IsNew) { model.CompanyId = companies[0].CompanyId; }
            ViewBag.CompanyId = new SelectList(companies, "CompanyId", "CompanyName", model.CompanyId);
            ViewBag.ProjectTypes = new SelectList(new List<string> {
                                    "Blue Collar - Civil",
                                    "Blue Collar - Others",
                                    "White Collar - I",
                                    "White Collar - II" });
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Project model)
        {
            if (ModelState.IsValid)
            {
                var currUser = CurrentUser;
                model.UserId = currUser.UserId;
                var log = new ProjectLog
                {
                    UserId = currUser.UserId,
                    UserName = currUser.Name,
                    Log = "Status - " + model.Status
                };
                _db.ProjectLogs.Add(log);
                if (model.IsNew)
                {
                    var startMile = new Milestone
                    {
                        MilestoneType = MilestoneTypes.Start,
                        Name = "Customer Request",
                        ExpectedDate = model.StartDate,
                    };

                    var endMile = new Milestone
                    {
                        MilestoneType = MilestoneTypes.End,
                        Name = "Project Delivery",
                        ExpectedDate = model.DueDate,
                    };

                    model.Milestones.Add(startMile);
                    model.Milestones.Add(endMile);

                    model.ProjetLogs.Add(log);
                    _db.Projects.Add(model);
                }
                else
                {
                    foreach (var item in model.Requirements)
                    {
                        _db.Entry(item).State = System.Data.EntityState.Modified;
                    }
                    _db.Entry(model).State = System.Data.EntityState.Modified;
                    log.ProjectId = model.ProjectId;
                }
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }


    }
}
