using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Models;

namespace Rdt.CourseFinder.Services
{
    public class AuthenticationSrv: ServiceBase
    {

        RNGCryptoService _hashService = new RNGCryptoService();

        User _user = null;
        SignInRequest _request;


        public void UpdatePassowrd(string newPassword, string oldPassword)
        {
            var user = CurrentUser;
            if (_hashService.IsHashSame(oldPassword, user.Password))
            {
                user.Password = RNGCryptoService.CreateHash(newPassword);
                _db.Entry(user).State = System.Data.EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                throw new SimpleException("Password incorrect");
            }
        }

        public User AuthenticateUser(SignInRequest request)
        {
            _request = request;
            var password = request.Password;
            var email = request.Email.Trim();
            email = email.Trim();
            var user = _db.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new SimpleException("Invalid user name or password");
            }
            if (user.IsBlocked)
            {
                throw new SimpleException("User Account is Blocked");
            }
            if (user != null && _hashService.IsHashSame(password, user.Password))
            {
                _user = user;
                return user;
            }
            throw new SimpleException("Invalid user name or password");
        }

        public void SetAuthCookie()
        {
            string formattedRoles = String.Empty;
            formattedRoles = _user.Role.ToString();
            HttpContext context = HttpContext.Current;

            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: _user.UserId.ToString(),
                issueDate: DateTime.UtcNow,
                expiration: DateTime.UtcNow.AddMinutes(660),
                isPersistent: true,
                userData: formattedRoles
                );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var formsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                //Secure = _configuration.RequireSSL
            };
            context.Response.Cookies.Add(formsCookie);

            //if (_configuration.RequireSSL)
            //{
            //    // Drop a second cookie indicating that the user is logged in via SSL (no secret data, just tells us to redirect them to SSL)
            //    context.Response.Cookies.Add(new HttpCookie(ForceSSLCookieName, "true"));
            //}
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            UserSession.RemoveAll();
            UserSession.UpdateCurrentUser("", null);
        }
    }
}