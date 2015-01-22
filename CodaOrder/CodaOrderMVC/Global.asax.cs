using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication3.Infrastructure;
using WebMatrix.Data;
using WebMatrix.WebData;

namespace WebApplication3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["identityConnection"];
            WebSecurity.InitializeDatabaseConnection(cs.Name, "UserProfile", "UserID", "UserName", true);
            //// Codajson
            //            CREATE TABLE CodaJson
            //(
            //OID          bigint NOT NULL PRIMARY KEY,
            //FullName		varchar (128) NOT NULL 
            //)

            AreaRegistration.RegisterAllAreas();
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}



// MVC Security
//- WebMatrix.Data
//- WebMatrix.WebData
//- microsoft.aspnet.webhelpers