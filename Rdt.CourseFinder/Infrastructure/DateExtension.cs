using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder
{
    public static class DateExtension
    {
        public static string Tommddyy(this DateTime date)
        {
            return date.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}