using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using UCDArch.Web.IoC;
using UCDArch.Web.ModelBinder;
using UCDArch.Data.NHibernate;
using Dogbert2.Controllers;
using Dogbert2.Core.Domain;
using Microsoft.Practices.ServiceLocation;
using Dogbert2.Helpers;

namespace Dogbert2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
#if DEBUG
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif

            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.DefaultBinder = new UCDArchModelBinder();

            NHibernateSessionConfiguration.Mappings.UseFluentMappings(typeof(Project).Assembly);

            IWindsorContainer container = InitializeServiceLocator();

            AutomapperConfig.Configure();
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