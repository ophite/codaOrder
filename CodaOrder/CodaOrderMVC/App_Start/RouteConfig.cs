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
            //    name: "DocumentsPost",
            //    url: "documents/{action}/{id}",
            //    defaults: new
            //    {
            //        controller = "Document",
            //        action = "Index",
            //        id = UrlParameter.Optional
            //    }
            //);
            //routes.MapRoute(
            //    name: "Search",
            //    url: "{controller}/{action}/{searchText}",
            //    defaults: new { controller = "SearchCodaObject", action = "Subject", searchText = UrlParameter.Optional }
            //);
            //routes.MapRoute(
            //    name: "Templates",
            //    url: "template/{action}/{id}",
            //    defaults: new { controller = "Template", action = "Index", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { action = MVC.Document.ActionNames.Index, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Not found",
                url: "{*.}",
                defaults: new { controller = MVC.Error.Name, action = MVC.Error.ActionNames.NotFound }
            );
        }
    }
}