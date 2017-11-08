using System.Web.Mvc;
using InvestAdvisor.Services;
using InvestAdvisor.Services.Contracts;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using InvestAdvisor.Data.Contracts;
using InvestAdvisor.Data;

namespace InvestAdvisor.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IImageRepository, ImageRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();

            container.RegisterType<IViewModelService,ViewModelService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}