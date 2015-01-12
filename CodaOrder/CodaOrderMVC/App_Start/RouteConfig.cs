using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //   name: "Template",
            //   url: "documents/template/{template_subfolder}/{action}.html",
            //   defaults: new { controller = "Template", action = "Index"},
            //   constraints: new { template_subfolder = @"\w+" }
            //);
            routes.MapRoute(
                name: "Documents",
                url: "documents/{action}/{id}",
                defaults: new { controller = "JournalSale_Documents", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Search",
                url: "{controller}/{action}/{searchText}",
                defaults: new { controller = "SearchCodaObject", action = "Subject", searchText = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "Templates",
            //    url: "template/{action}/{id}",
            //    defaults: new { controller = "Template", action = "Index", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

