using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Models;
using System.Text;
using System.IO;

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
        public ActionResult Index(ProjectSearchVm model, string output)
        {
            model.FillMasterList();
            model.ApplyFilters(Request.Form);

            if (output == "excel")
            {
                StringBuilder csvData = new StringBuilder();
                csvData.AppendLine(("Name,Category,PP#,BS (OMR),Age,Relevent Exp,Grade,Contact#,Place,Date,Agent,Status,Remarks"));
                foreach (var item in model.FilteredItems)
                {
                    if (item.Experience != null)
                    {
                        item.Experience = item.Experience.Replace(',', '|');
                    }
                    csvData.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", 
                        item.Name, item.Category, item.Passport, item.Bsro, item.Age, item.Experience, item.Grade, item.ContactNo, item.City, item.SeleDate, item.Agent, item.CandidateStatus.Name, item.Remarks));
                }
                var byteArray = Encoding.ASCII.GetBytes(csvData.ToString());
                var stream = new MemoryStream(byteArray);
                return File(stream, "text/plain", "RH_" + DateTime.Now.ToString("MMM_dd") + ".csv");
            }
            return View(model);
        }

    }
}
