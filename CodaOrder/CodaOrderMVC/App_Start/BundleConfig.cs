using System.Web;
using System.Web.Optimization;

namespace WebApplication3
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            // angular
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular_source/angular-resource.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-ui").Include(
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/Scripts/angular-ui/ui-bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/ng-grid").Include(
                "~/Scripts/ng-grid.js"
            ));

            // helpers
            bundles.Add(new ScriptBundle("~/bundles/linq").Include(
                "~/Scripts/linq.js"
            ));

            // angular app
            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory("~/Scripts/app", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/subject").IncludeDirectory("~/Scripts/app/subject", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/document").IncludeDirectory("~/Scripts/app/document", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/searchCodaObject").IncludeDirectory("~/Scripts/app/search", "*.js"));
            bundles.Add(new ScriptBundle("~/bundles/common").IncludeDirectory("~/Scripts/app/common", "*.js"));

            // css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/ng-grid.css",
                      "~/Content/angular_grid_custom.css"));
        }
    }
}
