using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public static PageTypes CurrentPage { get; protected set; }

        protected AtsDbContext _db = new AtsDbContext();
        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
            base.Dispose(disposing);
        }


        protected void AddModelError(SimpleException ex)
        {
            ModelState.AddModelError("", ex.Message);
        }


        protected int CurrentUserId
        {
            get
            {
                var userId = UserSession.CurrentUserId;
                return userId;
            }
        }

        protected User CurrentUser
        {
            get
            {
                var user = _db.Users.Find(CurrentUserId);
                return user;
            }
        }

        protected User GetCurrentUser()
        {
            var user = _db.Users.Find(CurrentUserId);
            return user;
        }

        protected bool IsAuth
        {
            get
            {
                try
                {
                    var stat = System.Web.HttpContext.Current.Request.IsAuthenticated;
                    return stat;
                }
                catch
                {
                    return false;
                }

            }
        }


        protected string HostAdress
        {
            get
            {
                return Request.UserHostAddress;
            }
        }


        [NonAction]
        protected List<string> GetModelStateErrorMsgs()
        {
            return ModelState.SelectMany(ms => ms.Value.Errors).Select(e => e.ErrorMessage).ToList();
        }


        [NonAction]
        protected JsonResult GetErrorMsgJSON()
        {
            var errMsgs = GetModelStateErrorMsgs();
            var jsonResult = new
            {
                errMsg = errMsgs.Any() ? errMsgs[0] : null,
            };
            return Json(jsonResult);
        }

        [NonAction]
        public virtual ActionResult SafeRedirect(string url = null, bool isAuth = false)
        {
            return Redirect(GetRedirectUrl(url, isAuth));
        }

        public virtual string GetRedirectUrl(string url = null, bool pIsAuth = false)
        {
            if (!String.IsNullOrWhiteSpace(url)
                && Url.IsLocalUrl(url)
                && url.Length > 1
                && url.StartsWith("/", StringComparison.Ordinal)
                && !url.StartsWith("//", StringComparison.Ordinal)
                && !url.StartsWith("/\\", StringComparison.Ordinal))
            {
                return url;
            }

            if (pIsAuth || IsAuth) 
            {
                switch (CurrentUser.Role)
                {
                    case UserRoleType.Recruiter:
                    case UserRoleType.Travel:
                    case UserRoleType.Finance:
                        url = Routes.NavigationItems[PageTypes.Dashboard].TinyUrl;
                        break;
                    case UserRoleType.Admin:
                    default:
                        url = Routes.NavigationItems[PageTypes.Projects].TinyUrl;
                        break;
                }
                //var request = System.Web.HttpContext.Current.Request;
                //var context = System.Web.HttpContext.Current;
                //HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];
                //if (authCookie != null)
                //{
                //    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                //    var roles = authTicket.UserData.Split('|');
                //    url = Routes.NavigationItems[PageTypes.Find].TinyUrl;
                //}
            }
            url = String.IsNullOrWhiteSpace(url) ? Routes.RootHome : url;
            return url;
        }

        [HttpGet]
        public JsonResult GetCountryNames(string term)
        {
            var items = AcHelper.CountryNames(term);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAgents(string term)
        {
            var items = AcHelper.Agents(term);
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetProjects(string term)
        {
            var items = AcHelper.ProjectNames(term);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AcCategoryNames(string term)
        {
            var items = AcHelper.Categories(term);
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        protected void AddUserLog(string msg)
        {
            var activityLog = CurrentUser.CreateLog(msg);
            _db.UserLogs.Add(activityLog);
            _db.SaveChanges();
        }
    }
}
