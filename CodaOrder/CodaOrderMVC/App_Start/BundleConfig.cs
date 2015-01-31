using System.Web;
using System.Web.Optimization;
using System.Linq;

namespace WebApplication3
{
    public class BundleHelper
    {
        private static readonly string _bundles = "~/bundles/";
        public static readonly string JQUERY = _bundles + "jquery";
        public static readonly string JQUERY_VALIDATE = _bundles + "jqueryval";
        public static readonly string MODERNIZR = _bundles + "modernizr";
        public static readonly string BOOTSTRAP = _bundles + "bootstrap";
        public static readonly string ANGULAR = _bundles + "angular";
        public static readonly string ANGULAR_UI = _bundles + "angular-ui";
        public static readonly string ANGULAR_NG_GRID = _bundles + "ng-grid";
        public static readonly string LINQ = _bundles + "linq";

        public static readonly string APP = _bundles + "app";
        public static readonly string APP_SUBJECT = _bundles + "app_subject";
        public static readonly string APP_DOCUMENT = _bundles + "app_document";
        public static readonly string APP_DOCUMENT_DIRECTIVE = _bundles + "app_document_directive";
        public static readonly string APP_SEARCH_CODA_OBJECT = _bundles + "app_searchCodaObject";
        public static readonly string APP_COMMON = _bundles + "app_common";

        private static readonly string _content = "~/Content/";
        public static readonly string CSS = _content + "css";
    }

    public class BundleConfig
    {
        public static Bundle IncludeT4MVC(Bundle bundle, params string[] virtualPaths)
        {
            bundle.Include(virtualPaths.Select(path => T4MVCPathToServerPath(path)).ToArray());
            return bundle;
        }

        public static string T4MVCPathToServerPath(string path)
        {
            return VirtualPathUtility.ToAppRelative(path);
        }

        public static Bundle IncludeT4MVCDirectoryJS(Bundle bundle, string directory)
        {
            return bundle.IncludeDirectory(T4MVCPathToServerPath(directory), "*.js");
        }

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(IncludeT4MVC(new ScriptBundle(BundleHelper.JQUERY), Links.Scripts.jquery_2_1_1_js));
            bundles.Add(IncludeT4MVC(new ScriptBundle(BundleHelper.JQUERY_VALIDATE), Links.Scripts.jquery_validate_js));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(IncludeT4MVC(new ScriptBundle(BundleHelper.MODERNIZR), Links.Scripts.modernizr_2_6_2_js));
            bundles.Add(IncludeT4MVC(new ScriptBundle(BundleHelper.BOOTSTRAP), Links.Scripts.bootstrap_js));

            // angular
            bundles.Add(IncludeT4MVC(new ScriptBundle(BundleHelper.ANGULAR),
                Links.Scripts.angular_js,
                Links.Scripts.angular_source.angular_resource_js,
                Links.Scripts.angular_source.angular_sanitize_js));

            bundles.Add(IncludeT4MVC(new ScriptBundle(BundleHelper.ANGULAR_UI),
                Links.Scripts.angular_ui.ui_bootstrap_tpls_js,
                Links.Scripts.angular_ui.ui_bootstrap_js,
                Links.Scripts.angular_ui.ui_select_js));

            bundles.Add(IncludeT4MVC(new ScriptBundle(BundleHelper.ANGULAR_NG_GRID), Links.Scripts.ng_grid_js));

            // helpers
            bundles.Add(IncludeT4MVC(new ScriptBundle(BundleHelper.LINQ), Links.Scripts.linq_js));

            // angular app
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP), Links.Scripts.app.Url()));
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_SUBJECT), Links.Scripts.app.subject.Url()));
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_DOCUMENT), Links.Scripts.app.document.Url()));
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_DOCUMENT_DIRECTIVE), Links.Scripts.app.document.directive.Url()));
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_SEARCH_CODA_OBJECT), Links.Scripts.app.search.Url()));
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_COMMON), Links.Scripts.app.common.Url()));

            // css
            bundles.Add(IncludeT4MVC(new StyleBundle(BundleHelper.CSS),
                Links.Content.bootstrap_css,
                Links.Content.Site_css,
                Links.Content.ng_grid_css,
                Links.Content.angular_grid_custom_css,
                Links.Content.ui_select_css));
        }
    }
}
