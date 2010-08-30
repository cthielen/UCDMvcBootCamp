﻿using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Castle;
using UCDArch.Web.IoC;
using UCDArch.Web.ModelBinder;
using UCDArch.Web.Validator;
using UCDMvcBootCamp.Controllers;

namespace UCDMvcBootCamp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

// ReSharper disable InconsistentNaming
        protected void Application_Start()
// ReSharper restore InconsistentNaming
        {
            #if DEBUG
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            #endif

            xVal.ActiveRuleProviders.Providers.Add(new ValidatorRulesProvider());

            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.DefaultBinder = new UCDArchModelBinder();

            IWindsorContainer container = InitializeServiceLocator();
        }

        private static IWindsorContainer InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

            return container;
        }
    }
}