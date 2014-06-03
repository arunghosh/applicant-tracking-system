using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Services
{
    public class UserService : ServiceBase
    {
        public User FindUserByEmail(string email)
        {
            email = email.Trim();
            var user = _db.Users.SingleOrDefault(u => u.Email == email);
            return user;
        }

        public void ValidateEmailUnique(string email)
        {
            var user = FindUserByEmail(email);
            if (user != null)
            {
                throw new SimpleException(Strings.EmailAlreadyInUse, email);
            }
        }
    }
}