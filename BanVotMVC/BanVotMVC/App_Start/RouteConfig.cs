using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanVotMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Category",
                url: "san-pham/{MetaTitle}-{CategoryId}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );

            routes.MapRoute(
                name: "Detail",
                url: "chi-tiet/{MetaTitle}-{ProductId}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );
            routes.MapRoute(
                name: "Add-to-cart",
                url: "them-gio-hang/{Id}-{Quantity}",
                defaults: new { controller = "Cart", action = "AddToCart", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );

            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );

            routes.MapRoute(
                name: "Success",
                url: "thanh-cong",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BanVotMVC.Controllers" }
            );
        }
    }
}
