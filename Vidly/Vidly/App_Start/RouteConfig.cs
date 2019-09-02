using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Custom route, määritelty Controllerissa [Route(jotain/jotain jne.)]
            routes.MapMvcAttributeRoutes();


            // Alkuperäinen route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                // Jos url ei sisällä actionia niin sitten valitaan kyseisen Controllerin Index-action
                // Id on valinnainen parametri, ei ole pakko antaa
            );

            /* Huono/vanha tyyli tehdä custom route
             * 
                // Lisätään custom route (ennen defaulttia, järjestyksellä on väliä)
                routes.MapRoute(
                "MoviesByReleaseDate",
                "movies/released/{year}/{month}",
                new { controller = "Movies", action = "ByReleaseDate" },
                // määritellään millaisia year ja month saavat olla, d = digit
                // voi tehdä joko  @"\d{4}" tai  "\\d{4}"
                // jos halutaan rajata esim. vuosi voi olla vain 2015 tai 2016 niin year = @"2015|2016"
                new { year = @"\d{4}", month = @"\d{2}" });
            */
        }
    }
}
