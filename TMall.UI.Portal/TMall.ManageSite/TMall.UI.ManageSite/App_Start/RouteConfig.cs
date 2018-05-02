using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TMall.Infrastructure.SearchModel.MvcExtensions;
using TMall.Infrastructure.SearchModel;

namespace TMall.UI.ManageSite
{
    public class RouteConfig
    {
        private const string NAMESPACE = "TMall.ManageSite.Controllers";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Index",
                url: "index",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { NAMESPACE }
                );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { NAMESPACE }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { NAMESPACE }
            );

            ModelBinders.Binders.Add(typeof(SearchCondition), new SearchModelBinder());
        }
    }
}