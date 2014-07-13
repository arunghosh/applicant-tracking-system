using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder
{
    public static class Routes
    {
        public const string Root = "http://nitcalumni.com/";
        public static Dictionary<PageTypes, NavigationItem> NavigationItems { get; set; }
        public const string RootHome = @"~\";

        public const string AcCountry = "GetCountryNames";
        public const string AcAgent = "GetAgents";
        public const string AcProjectName = "GetProjects";
        public const string AcCategory = "AcCategoryNames";


        public static string ImageUrl(string name)
        {
            return Root + "Content/Images/" + name;
        }

        public static string ExcelPath()
        {
            var path = HttpContext.Current.Server.MapPath(@"~\App_Data\r_" + DateTime.UtcNow.Ticks +".csv");
            return path;
        }

        public static string StatusFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\status.txt");
                return path;
            }
        }

        public static string CandidateFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\rh.csv");
                return path;
            }
        }

        public static string CategoryFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\ctgrys.txt");
                return path;
            }
        }

        public static string XlsFile
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(@"~\App_Data\rh.xlsx");
                return path;
            }
        }

        public static string GetTitile(PageTypes pageType)
        {
            var page = NavigationItems[pageType];
            var title = page.Titile;
            var displayTxt = page.DisplayText;
            return (string.IsNullOrEmpty(title) ? displayTxt : title) + " | RH";
        }
        
        static Routes()
        {
            NavigationItems = new Dictionary<PageTypes, NavigationItem>();

            //NavigationItems.Add(PageTypes.Login, new NavigationItem
            //{
            //    DisplayText = "Login",
            //    Controller = "Home",
            //    Action = "Login",
            //    PageType = PageTypes.Login,
            //    Titile = "User Login"
            //});


            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Companies",
                Controller = "Client",
                Action = "Index",
                PageType = PageTypes.Company,
                Titile = "Companies",
                ImageUrl="company.png"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Search",
                Controller = "Search",
                Action = "Index",
                PageType = PageTypes.Find,
                Titile = "Search Candidate",
                ImageUrl="search.png"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Projects",
                Controller = "Project",
                Action = "Index",
                PageType = PageTypes.Projects,
                Titile = "Projects",
                ImageUrl = "project.png"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Users",
                Controller = "User",
                Action = "Index",
                PageType = PageTypes.Users,
                Titile = "Users",
                ImageUrl = "user.png"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Activity Logs",
                Controller = "User",
                Action = "Logs",
                PageType = PageTypes.ActivityLog,
                ImageUrl = "log.png"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Imports",
                Controller = "Import",
                Action = "Index",
                PageType = PageTypes.Imports,
                Titile = "Excel Imports",
                ImageUrl = "import.png"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Dashboard",
                Controller = "Dashboard",
                Action = "Index",
                PageType = PageTypes.Dashboard,
                Titile = "Dashboard",
                ImageUrl = "Dashboard.png"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Report",
                Controller = "Report",
                Action = "Index",
                PageType = PageTypes.Report,
                Titile = "Report",
                ImageUrl = "report.png"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Settings",
                Controller = "Settings",
                Action = "Index",
                PageType = PageTypes.Setting,
                Titile = "Settings",
                ImageUrl = "settings.png"
            });

            AddCommonRoute(new NavigationItem
            {
                DisplayText = "Calendar",
                Controller = "Calendar",
                Action = "Index",
                PageType = PageTypes.TravelCalendar,
                Titile = "Travel Calendar",
                ImageUrl = "Calendar.png"
            });
        }

        private static void AddCommonRoute(NavigationItem navItem)
        {
            navItem.Area = "";
            NavigationItems.Add(navItem.PageType, navItem);
        }
    }
}