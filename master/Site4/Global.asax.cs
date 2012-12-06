﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using Portfotolio.DependencyInjection;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Services.Logging;

namespace Portfotolio.Site4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private IDependencyEngine _dependencyEngine;
        private readonly Logger _logger;

        public MvcApplication()
        {
            _logger = LogManager.GetLogger("Application");
        }

        protected void Application_Start()
        {
            _logger.Info("Application Started.");
            HttpContext.Current.Application[DataKeys.ApplicationStarted] = DateTime.Now;

            AreaRegistration.RegisterAllAreas();

            _dependencyEngine = DependencyEngineFactory.Create();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            _logger.Info("Application Ended.");

            if (_dependencyEngine != null)
            {
                _dependencyEngine.Dispose();
                _dependencyEngine = null;
            }
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            _logger.LogException(exception);

            Server.Transfer("~/Content/error.htm");
        }
    }
}