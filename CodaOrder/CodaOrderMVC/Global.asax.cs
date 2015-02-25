using iOrder.Infrastructure;
using iOrder.ModelBinder;
using iOrder.Models;
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
using WebMatrix.Data;
using WebMatrix.WebData;

namespace iOrder
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IdentityContext.InitializeDatabase();
            AreaRegistration.RegisterAllAreas();
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(LinesViewModel), new LineBinder());
            ModelBinders.Binders.Add(typeof(CodaUserProfile), new UserProfileBinder());
        }
    }
}

// MVC Security
//- WebMatrix.Data
//- WebMatrix.WebData
//- microsoft.aspnet.webhelpers