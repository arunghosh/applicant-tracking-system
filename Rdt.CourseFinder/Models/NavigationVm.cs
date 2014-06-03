using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Models
{
    public class NavigationVm
    {
        public IEnumerable<NavigationItem> ImageItems { get; set; }
        public IEnumerable<NavigationItem> TextItems { get; set; }
        public PageTypes SelectePage { get; set; }
    }
}