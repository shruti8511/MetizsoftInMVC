using Metizsoft.Service;
using Metizsoft.Service.Implementation;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Metizsoft
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ICommon, CommonService>();
            container.RegisterType<IAuthorizeAccess, AuthorizeAccessService>();
            container.RegisterType<IRetail, RetailerService>();
            container.RegisterType<IMaster, MasterServices>();
            container.RegisterType<IDoctor, DoctorService>();
            container.RegisterType<IProduct, ProductService>();
            container.RegisterType<IReport, ReportService>();
            container.RegisterType<IExpenditure, ExpenditureService>();
            container.RegisterType<ILeave, LeaveServices>();
            container.RegisterType<IEvent, EventService>();
            container.RegisterType<IGroup, Groupservice>();
            container.RegisterType<ITourProgram, TourProgramService>();
            container.RegisterType<INotification, NotificationService>();
            container.RegisterType<IAccount, AccountService>();          
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}