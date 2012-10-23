﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Portfotolio.Domain;
using Portfotolio.Site.DependencyInjection;
using Portfotolio.Site.Helpers;
using Portfotolio.Utility.DependencyInjection;

namespace Portfotolio.Site.Mvc
{
    public static class MvcConfigurator
    {
         public static void RegisterViewEngines()
         {
             var engines = ViewEngines.Engines;

             engines.Clear();
             engines.Add(new CSharpRazorViewEngine());
         }

        public static void RegisterGlobalFilters()
        {
            var filters = GlobalFilters.Filters;
            
            filters.Add(new MasterHandleErrorAttribute());
            filters.Add(new SetMasterViewDataAttribute());
        }

        public static void RegisterRoutes()
        {
            var routes = RouteTable.Routes;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Home",
                "",
                new { controller = "photo", action = "interestingness" });

            routes.MapRoute(
                "Group", // Route name
                "group/{id}", // URL with parameters
                new { controller = "photo", action = "group", id = UrlParameter.Optional } // Parameter defaults
                );

            routes.MapRoute(
                "Photo", // Route name
                "{id}/{action}/{secondaryId}", // URL with parameters
                new { controller = "photo", action = "photos", secondaryId = UrlParameter.Optional }, // Parameter defaults
                new { id = "^[^-].*" }
                );

            routes.MapRoute(
                "Non-Photo", // Route name
                "-{controller}/{action}/{id}", // URL with parameters
                new { action = "photos", id = UrlParameter.Optional } // Parameter defaults
                );
        }

        public static IDependencyEngine RegisterDependencyInjectionFramework()
        {
            var dependencyEngine = new WindsorDependencyEngine();
            dependencyEngine.RegisterComponents();
            DependencyResolver.SetResolver(new DependencyEngineWrapper(dependencyEngine));
            return dependencyEngine;
        }
    }
}