using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eCart
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Admin_Default",
            //    url: "Admin/{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "eCart.Areas.Admin.Controllers" }
            //);

            //routes.MapRoute(
            //    name: "Shopper_Default",
            //    url: "Shopper/{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "eCart.Areas.Shopper.Controllers" }
            //);

            //routes.MapRoute(
            //    name: "Store_Default",
            //    url: "Store/{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "eCart.Areas.Stoer.Controllers" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "eCart.Controllers" }
            );
        }
    }
}
