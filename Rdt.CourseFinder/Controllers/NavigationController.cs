using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Infrastructure;
using Rdt.CourseFinder.Models;

namespace Rdt.CourseFinder.Controllers
{
    public class NavigationController : BaseController
    {
        public PartialViewResult Menu()
        {
            var user = CurrentUser;
            IEnumerable<NavigationItem> navItems = new List<NavigationItem>();
            var pages = RolePagesMap.Pages;
            navItems = navItems.Concat(pages[user.Role]);
            var txtItems = navItems.Where(n => string.IsNullOrEmpty(n.ImageUrl));
            var imgItems = navItems.Where(n => !string.IsNullOrEmpty(n.ImageUrl));
            var vm = new NavigationVm
            {
                TextItems = txtItems,
                ImageItems = imgItems,
                SelectePage = CurrentPage,
            };
            return PartialView(vm);
        }


    }
}
