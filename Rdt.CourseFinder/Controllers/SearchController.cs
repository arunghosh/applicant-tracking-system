using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Models;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Controllers
{
    public class SearchController : BaseController
    {
        public PartialViewResult NameSearch()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            CurrentPage = PageTypes.Find;
            var model = new CandidateSearchVm();
            model.ApplyFilters();
            model.FilteredItems = new List<Candidate>();
            return View(model);
        }

        [HttpPost]
        public ViewResult Index(CandidateSearchVm model, string output)
        {
            CurrentPage = PageTypes.Find;
            model.ApplyFilters(Request.Form);
            if (output == "mail")
            {
                var html = new HtmlComposer();
                foreach (var item in model.Filters.Skip(3).Take(2))
                {
                    html.AppendRaw("<table border='1' style='text-align:left' cellpadding:'2'>");
                    html.AppendRaw("<tr>");
                    html.AppendRaw("<th  style='width:100px;'>" + item.Name + "</th><th style='width:100px;'>Count</th>");
                    html.AppendRaw("</tr>");
                    foreach (var fItem in item.FilterItems)
                    {
                        html.AppendRaw("<tr>");
                        html.AppendDiv("<td>" + fItem.ValueText + "</td><td>" + fItem.Count + "</td>");
                        html.AppendRaw("</tr>");
                    }
                    html.AppendRaw("</table>");
                    html.AppendBr();
                }
                ViewBag.Stat = html.Text;
                return View("Mail");
            }
            return View(model);
        }
    }
}
