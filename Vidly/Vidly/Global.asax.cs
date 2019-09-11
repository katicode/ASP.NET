using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http; // API tarvitsee
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Vidly
{
    public class MvcApplication : System.Web.HttpApplication
    {
        // Kun sovellus avataan niin kutsutaan tätä metodia
        protected void Application_Start()
        {
            // APIn konfigurointi
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            
            //Kerrotaan sovellukselle reititys
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
