using Metizsoft.Service;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace Metizsoft.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUser, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMaster, MasterServices>(new HierarchicalLifetimeManager());

           
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}