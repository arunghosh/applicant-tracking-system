using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Controllers
{
    public class ImportController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            CurrentPage = PageTypes.Imports;
            var imports = _db.Imports.ToList();
            return View(imports);
        }


        [HttpGet]
        public ViewResult Edit()
        {
            CurrentPage = PageTypes.Imports;

            var model = new Import();
            FillProjects(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Import model)
        {
            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].FileName.ToLower().Contains(".xls"))
                {
                    HttpPostedFileBase file = Request.Files[0];
                    using (var srv = new ImportSrv(file, model))
                    {
                        srv.SaveToDB();
                        ViewData[Constants.ViewDataMsgKey] = srv.Summary;
                    }
                }
            }
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}

            catch (SimpleException ex)
            {
                ViewData[Constants.ViewDataMsgKey] = "Import Failed.\n" + ex.Message;    
            }
            catch (Exception ex)
            {
                ViewData[Constants.ViewDataMsgKey] = "Import Failed.\n" + ex.Message;    
            }
            FillProjects(model);
            return View(model);
        }

        private void FillProjects(Import model)
        {
            var projects = _db.Projects.ToList();
            ViewBag.ProjectId = new SelectList(projects, "ProjectId", "ProjectName", model.ProjectId == 0 ? projects[0].ProjectId : model.ProjectId);
        }
    }
}
