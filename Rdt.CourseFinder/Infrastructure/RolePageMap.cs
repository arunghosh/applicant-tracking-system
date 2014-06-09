using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Infrastructure
{
    public static class RolePagesMap
    {
        private static List<NavigationItem> _commonPages = new List<NavigationItem>();
        public static List<NavigationItem> CommonPages
        {
            get { return _commonPages; }
            set { _commonPages = value; }
        }


        static Dictionary<UserRoleType, List<NavigationItem>> _pages = new Dictionary<UserRoleType, List<NavigationItem>>();
        public static Dictionary<UserRoleType, List<NavigationItem>> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        static RolePagesMap()
        {
            var adminPages = new List<NavigationItem>();

            adminPages.Add(Routes.NavigationItems[PageTypes.Dashboard]);
            adminPages.Add(Routes.NavigationItems[PageTypes.Company]);
            adminPages.Add(Routes.NavigationItems[PageTypes.Projects]);
            adminPages.Add(Routes.NavigationItems[PageTypes.Imports]);
            adminPages.Add(Routes.NavigationItems[PageTypes.TravelCalendar]);
            adminPages.Add(Routes.NavigationItems[PageTypes.Users]);
            adminPages.Add(Routes.NavigationItems[PageTypes.Setting]);
            _pages.Add(UserRoleType.Admin, adminPages);

            var recruitPages = new List<NavigationItem>();
            recruitPages.Add(Routes.NavigationItems[PageTypes.Dashboard]);
            recruitPages.Add(Routes.NavigationItems[PageTypes.Imports]);
            recruitPages.Add(Routes.NavigationItems[PageTypes.ActivityLog]);
            _pages.Add(UserRoleType.Recruiter, recruitPages);

            var travelPages = new List<NavigationItem>();
            travelPages.Add(Routes.NavigationItems[PageTypes.Dashboard]);
            travelPages.Add(Routes.NavigationItems[PageTypes.TravelCalendar]);
            travelPages.Add(Routes.NavigationItems[PageTypes.ActivityLog]);
            _pages.Add(UserRoleType.Travel, travelPages);


            var financePages = new List<NavigationItem>();
            financePages.Add(Routes.NavigationItems[PageTypes.Dashboard]);
            financePages.Add(Routes.NavigationItems[PageTypes.ActivityLog]);
            _pages.Add(UserRoleType.Finance, financePages);

        }
    }

}