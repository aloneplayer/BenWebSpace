using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcLibrary
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);
            routes.MapRoute(
                "HHTest1",
                "MyAction/{para}",
                new { controller = "MyController", action = "Action" });

            routes.MapRoute(
               "HHTest2",
               "{aaa}/{bbb}/{ccc}"
                                   );

            // Archive/2008-05-07
            routes.MapRoute(
                "BlogArchive",
                "Archive/{date}",
                new { controller = "Blog", action = "Archive" },
                new { date = @"^\d{4}-\d{2}-\d{2}" });

            // Car/bmw.abc
            routes.MapRoute(
                "Car",
                "Car/{make}.{model}",
                new { controller = "Car", action = "Index" },
                new { make = @"(acural|bmw)" });

            routes.MapRoute(
                "Book",
                "Book/Add/{name}",
                new { controller = "Book", action = "Add" },
                new { httpMethod = "POST" });

            routes.MapRoute(
                "CatchIt",
                "Product/{*values}",
                new { controller = "Product", action = "Index" });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            //==HHTest: 就算是存在物理文件，仍要进行Routing
            RouteTable.Routes.RouteExistingFiles = true;

            //==HHTest: using route debugger
            RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);

        }

        private Route CreateRoute()
        {
            //Url 
            string url  = "{controller}/{action}/{id}";

            //Default
            Dictionary<string, object> defaultDict = new Dictionary<string, object>();
            defaultDict["action"] = "Index";
            defaultDict["id"] = 0;
            RouteValueDictionary defaultRouteValue = new RouteValueDictionary(defaultDict);

            //Constraint
            RouteValueDictionary constraintRouteValue = new RouteValueDictionary();
            constraintRouteValue["controller"] = @"^\w+";
            constraintRouteValue["id"] = @"\d+";

            Route route = new Route(url, defaultRouteValue, constraintRouteValue, new MvcRouteHandler());

            return route;
        }
    }
}