using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kona.Data;
using Kona.Infrastructure;
using StructureMap;
using System.Reflection;
using System.Data.Linq;

namespace Kona.Web {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "CMS",                                              // Route name
                "{controller}/{action}/{slug}",                           // URL with parameters
                new { controller = "Home", action = "Index", slug = "" }  // Parameter defaults
            );
            
            routes.MapRoute(
                "Product",                                              // Route name
                "{controller}/{action}/{sku}",                           // URL with parameters
                new { controller = "Home", action = "Show", sku = "" }  // Parameter defaults
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start() {
            RegisterRoutes(RouteTable.Routes);
            Bootstrapper.ConfigureStructureMap();

            
            ControllerBuilder.Current.SetControllerFactory(
                new KonaControllerFactory()
                );

            ViewEngines.Engines.Add(new KonaViewEngine());


        }
    }
}