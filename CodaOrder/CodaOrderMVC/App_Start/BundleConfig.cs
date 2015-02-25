using System.Web;
using System.Web.Optimization;
using System.Linq;
using System.IO;

namespace iOrder
{
    public class BundleHelper
    {
        private static readonly string _bundles = "~/bundles/";
        public static readonly string APP_SUBJECT = _bundles + "app_subject";
        public static readonly string APP_DOCUMENT = _bundles + "app_document";
        public static readonly string APP_ACCOUNT = _bundles + "app_account";
        public static readonly string APP_SEARCH_CODA_OBJECT = _bundles + "app_searchCodaObject";
        public static readonly string APP_COMMON = _bundles + "app_common";
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

        public static Bundle IncludeT4MVCDirectoryJS(Bundle bundle, string directory, bool searchSubdirectories = false)
        {
            return bundle.IncludeDirectory(T4MVCPathToServerPath(directory), "*.js", searchSubdirectories);
        }

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // angular app
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_SUBJECT), Links.Scripts.app.subject.Url()));
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_DOCUMENT), Links.Scripts.app.document.Url(), true));
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_ACCOUNT), Links.Scripts.app.account.Url(), true));
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_SEARCH_CODA_OBJECT), Links.Scripts.app.search.Url()));
            bundles.Add(IncludeT4MVCDirectoryJS(new ScriptBundle(BundleHelper.APP_COMMON), Links.Scripts.app.common.Url()));
        }
    }
}
