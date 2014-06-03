using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Models;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (!_db.Users.Any())
            {
                var user = new User
                {
                    Email = "admin@rh.com",
                    Name = "Admin",
                    Role = UserRoleType.Admin,
                    Password = RNGCryptoService.CreateHash("abcd1234"),
                };
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            var temp = DbCache.Instance.CanditStatus;
            if (IsAuth)
            {
                return SafeRedirect();
            }
            return View(new SignInRequest());
        }

        //[HttpGet]
        //public JsonResult Status(int id, string stat)
        //{
        //    var user = _db.Candidates.Find(id);
        //    user.Status = stat;
        //    _db.Entry(user).State = EntityState.Modified;
        //    _db.SaveChanges();
        //    return Json(new { }, JsonRequestBehavior.AllowGet);
        //}

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(SignInRequest model, string returnUrl)
        {
            string redirectUrl = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    var ip = Request.UserHostAddress;
                    using (var authService = new AuthenticationSrv())
                    {
                        var user = authService.AuthenticateUser(model);
                        authService.SetAuthCookie();
                        var browser = Request.Browser.Browser + Request.Browser.Version;
                        var sessionId = HttpContext.Session.SessionID;
                        LogSessionAsync(browser, ip, user, sessionId);
                    }
                    return SafeRedirect(returnUrl, true);
                }
                catch (SimpleException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            using (var authService = new AuthenticationSrv())
            {
                var sessionId = HttpContext.Session.SessionID;
                LogOutSessionAsync(sessionId);
                authService.SignOut();
            }
            return RedirectToAction("Index");
        }


        void LogOutSessionAsync(string sessionId)
        {
            new Thread(new ThreadStart(() =>
            {
                using (var db = new AtsDbContext())
                {
                    try
                    {
                        var session = db.Sessions.Where(s => s.SessionId == sessionId)
                                        .ToList().Last();
                        if (session != null)
                        {
                            session.End = DateTime.UtcNow;
                            db.SaveChanges();
                        }
                    }
                    catch { }
                }
            })).Start();
        }

        void LogSessionAsync(string browser, string ip, User user, string sessionId)
        {
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    using (var db = new AtsDbContext())
                    {
                        var session = new Session();
                        session.Browser = browser;
                        session.IPAddress = ip;
                        session.UserName = user.Name;
                        session.Start = DateTime.UtcNow;
                        session.UserId = user.UserId;
                        session.SessionId = sessionId;

                        var pastActSession = db.Sessions.Where(s => s.UserId == user.UserId && s.End == null && s.IPAddress == ip);
                        foreach (var item in pastActSession)
                        {
                            item.End = DateTime.UtcNow;
                            db.Entry(item).State = System.Data.EntityState.Modified;
                        }

                        db.Sessions.Add(session);
                        db.SaveChanges();
                    }
                }
                catch { }
            })).Start();
        }



    }
}
