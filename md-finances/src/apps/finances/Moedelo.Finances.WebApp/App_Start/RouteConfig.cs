using System.Web.Mvc;
using System.Web.Routing;

namespace Moedelo.Finances.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Main", "", new { controller = "Finances", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("PaymentImportRules", "PaymentImportRules", new { controller = "Finances", action = "PaymentImportRules", id = UrlParameter.Optional });
            routes.MapRoute("Example", "Example", new { controller = "Finances", action = "Example", id = UrlParameter.Optional });
            routes.MapRoute("ExampleEgrip", "ExampleEgrip", new { controller = "Finances", action = "ExampleEgrip", id = UrlParameter.Optional });
        }
    }
}
