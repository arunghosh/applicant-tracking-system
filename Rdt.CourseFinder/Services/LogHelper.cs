using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Services
{
    public static class LogHelper
    {
        public static string CanidateUpdate(Candidate candidate)
        {
            return candidate.ChangeStatus + " candidate with passport #" + candidate.Passport;
        }
    }
}