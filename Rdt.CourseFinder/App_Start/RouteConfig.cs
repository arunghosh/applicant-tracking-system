using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rdt.CourseFinder
{
    public class RouteConfig
    {
        private static RouteCollection _routes;

        public static void RegisterRoutes(RouteCollection routes)
        {
            _routes = routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            foreach (PageTypes pageType in Enum.GetValues(typeof(PageTypes)))
            {
                AddRoute(Routes.NavigationItems[pageType]);
            }

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        private static void AddRoute(NavigationItem nav)
        {
            var alumniRoute = _routes.MapRoute(
                name: nav.Controller + nav.Action,
                url: nav.RoutingText + "/{id}",
                defaults: new { controller = nav.Controller, action = nav.Action, id = UrlParameter.Optional }
            );
            alumniRoute.DataTokens["area"] = nav.Area;
        }
    }
}