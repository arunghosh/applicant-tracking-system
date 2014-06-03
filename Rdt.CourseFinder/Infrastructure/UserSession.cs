using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder
{
    public static class UserSession
    {
        public static void RemoveAll()
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session.RemoveAll();
            }
            FormsAuthentication.SignOut();
        }

        public static string CurrentUserName
        {
            get
            {
                try
                {

                    using (var db = new AtsDbContext())
                    {
                        return db.Users.Find(CurrentUserId).Name;
                    }
                }
                catch
                {
                    using (var _authService = new AuthenticationSrv())
                    {
                        _authService.SignOut();
                        RemoveAll();
                    }
                    throw new SimpleException(Strings.SessionExpired);
                }
            }
        }

        public static HashSet<int> CartItems
        {
            get
            {
                var session = HttpContext.Current.Session;
                var sessionCart = session[Constants.CartKey];
                var cart = sessionCart == null
                                ? new HashSet<int>()
                                : sessionCart as HashSet<int>;
                return cart;
            }
            set
            {
                var session = HttpContext.Current.Session;
                session[Constants.CartKey] = value;
            }
        }

        public static void AddItemToCart(int id)
        {
            var cart = CartItems;
            cart.Add(id);
        }

        public static bool UpadateCart(int id)
        {
            var cart = CartItems;
            var status = false;
            if (cart.Contains(id))
            {
                cart.Remove(id);
            }
            else
            {
                cart.Add(id);
                status = true;
            }
            CartItems = cart;
            return status;
        }

        public static void RemoveItemFromCart(int id)
        {
            var cart = CartItems;
            cart.Remove(id);
        }

        public static int CurrentUserId
        {
            get
            {
                var id = GetCurrentUserId();
                if (id == null)
                {
                    throw new Exception(Strings.SessionExpired);
                }
                else
                {
                    return id ?? 0;
                }
            }
        }

        public static int? GetCurrentUserId()
        {
            var httpContext = System.Web.HttpContext.Current;
            if (httpContext.Session[SessionKeys.CurrentUserId] == null)
            {
                var context = System.Web.HttpContext.Current;
                var request = System.Web.HttpContext.Current.Request;
                HttpCookie authCookie = request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var roles = authTicket.UserData.Split('|');
                    UserSession.UpdateCurrentUser("", authTicket.Name);
                }
            }
            var userId = httpContext.Session[SessionKeys.CurrentUserId] as string;
            if (userId == null)
            {
                RemoveAll();
                return null;
            }
            return int.Parse(userId);// userInfo != null ? userInfo.id : null;
        }

        public static void UpdateCurrentUser(string userFullName, string userId)
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session[SessionKeys.CurrentUserId] = userId;
                //session[SessionKeys.CurrentUserName] = userFullName;
            }
        }


        public static List<CandidateStatus> CanidateStatus
        {
            get
            {
                using (var db = new AtsDbContext())
                {
                    var role = db.Users.Find(CurrentUserId).Role;
                    if (role == UserRoleType.Admin)
                    {
                        return DbCache.Instance.CanditStatus.ToList();
                    }
                    var statusList = DbCache.Instance.CanditStatus.Where(s => s.CanBeUpdatedBy == role || s.IsUpdateByAll).ToList();
                    return statusList;
                }
            }
        }
    }
}