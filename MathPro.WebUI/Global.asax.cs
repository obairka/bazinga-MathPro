using MathPro.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MathPro.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
       {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role

            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer()); 
            Database.SetInitializer<EFDbContext>(new EFDbInitializer());
 
        
        }
    }
}
