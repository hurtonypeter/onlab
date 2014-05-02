using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity.Migrations;
using Perseus.Filters;

namespace Perseus
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // inicializáljuk a MappingHelper-t
            Perseus.Models.MappingHelper.Initialize();

            // kidobáljuk a webforms engint, csak razor fusson
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());


            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContext, DbMigrationsConfiguration<DbContext>>());
        }
    }
}
