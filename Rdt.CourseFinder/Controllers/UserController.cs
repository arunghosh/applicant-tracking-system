using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;
using Rdt.CourseFinder.Models;

namespace Rdt.CourseFinder.Controllers
{
    public class UserController : BaseController
    {
        UserService _userSrv = new UserService();

        public PartialViewResult Password()
        {
            return PartialView(new PasswordVm());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Password(PasswordVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var srv = new AuthenticationSrv())
                    {
                        srv.UpdatePassowrd(model.NewPassword, model.CurrentPassword);
                    }
                }
                catch (SimpleException ex)
                {
                    AddModelError(ex);
                }
            }
            return GetErrorMsgJSON();
        }

        //
        // GET: /User/
        public ActionResult Index()
        {
            CurrentPage = PageTypes.Users;
            var users = _db.Users.ToList();
            return View(users);
        }


        public ViewResult Details(int id)
        {
            CurrentPage = PageTypes.Users;
            var userId = CurrentUserId;
            var user = _db.Users.Include(u => u.UserLogs).Single(u => u.UserId == id);;
            return View(user);
        }

        public ViewResult Logs()
        {
            CurrentPage = PageTypes.ActivityLog;
            var userId = CurrentUserId;
            var logs = _db.UserLogs.Where(l => l.ByUserId == userId || l.UserId == userId).ToList();
            return View(logs);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var model = id == null
                        ? new User()
                        : _db.Users.Find(id);
            ViewBag.Role = model.Role.ToSelectList();
            return PartialView(model.GetUserCreateVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(UserCreateVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string msg = string.Empty;
                    var user = model.IsNew 
                                ? new User()
                                : _db.Users.Find(model.UserId);
                    if (user.Email != model.Email)
                    {
                        _userSrv.ValidateEmailUnique(model.Email);
                    }
                    user.UpdateUserCreateVm(model);
                    if (model.IsNew)
                    {
                        _db.Users.Add(user);
                        user.Password = RNGCryptoService.CreateHash("rh1234");
                        msg = "User Created";
                    }
                    else
                    {
                        _db.Entry(user).State = System.Data.EntityState.Modified;
                        msg = "User Modified";
                    }
                    _db.SaveChanges();
                    _userSrv.AddUserLog(user.UserId, msg);
                }
                catch (SimpleException ex)
                {
                    AddModelError(ex);
                }
            }
            return GetErrorMsgJSON();
        }

    }
}
