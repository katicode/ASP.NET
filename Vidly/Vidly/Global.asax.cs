using AutoMapper; //Mapper vaatii
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http; // API tarvitsee
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Vidly.App_Start; // mapping profile l�ytyy t��lt�

namespace Vidly
{
    public class MvcApplication : System.Web.HttpApplication
    {
        // Kun sovellus avataan niin kutsutaan t�t� metodia
        protected void Application_Start()
        {
            // AutoMapperin k�ynnistys, set up code
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

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
