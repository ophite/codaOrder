using System.Web;
using System.Web.Optimization;
using System.Linq;
using System.IO;

namespace iOrder
{
    public class BundleHelper
    {
        private static readonly string _bundles = "~/bundles/";
        public static readonly string CSSBundle = _bundles + "StylesBundle";
        public static readonly string BaseBundle = _bundles + "baseBundle";
        public static readonly string AngularBundle = _bundles + "angularBundle";
        public static readonly string HelperBundle = _bundles + "helperBundle";
        public static readonly string AppBundle = _bundles + "appBundle";
    }

    public class BundleConfig
    {
        //public static Bundle IncludeT4MVC(Bundle bundle, params string[] virtualPaths)
        //{
        //    bundle.Include(virtualPaths.Select(path => T4MVCPathToServerPath(path)).ToArray());
        //    return bundle;
        //}

        public static string T4MVCPathToServerPath(string path)
        {
            return VirtualPathUtility.ToAppRelative(path);
        }

        public static Bundle IncludeT4MVCDirectoryJS(Bundle bundle, string directory, bool searchSubdirectories = false)
        {
            return bundle.IncludeDirectory(T4MVCPathToServerPath(directory), "*.js", searchSubdirectories);
        }

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // css
            StyleBundle css = new StyleBundle(BundleHelper.CSSBundle);
            bundles.Add(css.Include(new string[] { 
                T4MVCPathToServerPath(Links.Content.bootstrap_css), 
                T4MVCPathToServerPath(Links.Content.Site_css), 
                T4MVCPathToServerPath(Links.Content.ng_grid_css), 
                T4MVCPathToServerPath(Links.Content.angular_grid_custom_css), 
                T4MVCPathToServerPath(Links.Content.ui_select_css)
            }));

            // base
            ScriptBundle baseBundle = new ScriptBundle(BundleHelper.BaseBundle);
            bundles.Add(baseBundle.Include(new string[] { 
                T4MVCPathToServerPath(Links.Scripts._3rdparty.modernizr_2_6_2_js),
                T4MVCPathToServerPath(Links.Scripts.jquery.jquery_2_1_1_js),
                T4MVCPathToServerPath(Links.Scripts.jquery.jquery_validate_js),
                T4MVCPathToServerPath(Links.Scripts._3rdparty.bootstrap_js),
                T4MVCPathToServerPath(Links.Scripts.jquery.jquery_unobtrusive_ajax_js),
                T4MVCPathToServerPath(Links.Scripts.app.Constant_js),
            }));

            // angular
            ScriptBundle angularBundle = new ScriptBundle(BundleHelper.AngularBundle);
            bundles.Add(angularBundle.Include(new string[] {
                T4MVCPathToServerPath(Links.Scripts.angular_source.angular_js),
                T4MVCPathToServerPath(Links.Scripts.angular_source.angular_resource_js),
                T4MVCPathToServerPath(Links.Scripts.angular_source.angular_sanitize_js),
                T4MVCPathToServerPath(Links.Scripts.angular_source.angular_route_js),
                T4MVCPathToServerPath(Links.Scripts.angular_ui.ui_bootstrap_tpls_js),
                T4MVCPathToServerPath(Links.Scripts.angular_ui.ui_bootstrap_js),
                T4MVCPathToServerPath(Links.Scripts.angular_ui.ui_select_js),
                T4MVCPathToServerPath(Links.Scripts.angular_ui.angular_ui_router_js),
                T4MVCPathToServerPath(Links.Scripts._3rdparty.ng_grid_js),
            }));

            // helpers 
            ScriptBundle helpersBundle = new ScriptBundle(BundleHelper.HelperBundle);
            bundles.Add(helpersBundle.Include(new string[] {
                T4MVCPathToServerPath(Links.Scripts._3rdparty.Date_min_js),
                T4MVCPathToServerPath(Links.Scripts._3rdparty.linq_js),
                T4MVCPathToServerPath(Links.Scripts._3rdparty.spin_js),
                T4MVCPathToServerPath(Links.Scripts.angular_ui.angular_cache_js),
                T4MVCPathToServerPath(Links.Scripts.angular_ui.angular_spinner_js)
            }));

            // angular app
            ScriptBundle appBundle = new ScriptBundle(BundleHelper.AppBundle);
            bundles.Add(appBundle.Include(new string[] { 
                T4MVCPathToServerPath(Links.Scripts.app.app_js) 
            })
            .IncludeDirectory(T4MVCPathToServerPath(Links.Scripts.app.subject.Url()), "*.js", true)
            .IncludeDirectory(T4MVCPathToServerPath(Links.Scripts.app.document.Url()), "*.js", true)
            .IncludeDirectory(T4MVCPathToServerPath(Links.Scripts.app.account.Url()), "*.js", true)
            .IncludeDirectory(T4MVCPathToServerPath(Links.Scripts.app.search.Url()), "*.js", true)
            .IncludeDirectory(T4MVCPathToServerPath(Links.Scripts.app.common.Url()), "*.js", true)
            );
        }
    }
}
