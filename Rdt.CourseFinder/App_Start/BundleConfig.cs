using System.Web;
using System.Web.Optimization;

namespace Rdt.CourseFinder
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryAndRdt")
            .Include(
            "~/Scripts/jquery.slimscroll.js",
            "~/Scripts/jquery-ui-{version}.js",
            "~/Scripts/jquery.validate.min.js",
            "~/Scripts/_ajax-pop.js",
            "~/Scripts/_common.js",
            "~/Scripts/_time.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/themes/base/*.css")
                .Include("~/Content/*.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css")
                .Include("~/Content/themes/base/*.css"));
        }
    }
}