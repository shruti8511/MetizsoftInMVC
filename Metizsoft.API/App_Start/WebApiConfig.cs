using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using Unity;
using Metizsoft.Service;
using Unity.Lifetime;
using System.Web.Mvc;
using Unity.Mvc5;
using Metizsoft.Service.Implementation;
using Metizsoft.Service.Interface;



namespace Metizsoft.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUser, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMaster, MasterServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IDoctor, DoctorService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRetail, RetailerService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITourProgram, TourProgramService>(new HierarchicalLifetimeManager());
            container.RegisterType<IExpenditure, ExpenditureService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILeave, LeaveServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IEvent, EventService>(new HierarchicalLifetimeManager());
            container.RegisterType<INotification, NotificationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccount, AccountService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProduct, ProductService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

        }
    }
}
