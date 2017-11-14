using System.Web.Mvc;
using InvestAdvisor.Services;
using InvestAdvisor.Services.Contracts;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace InvestAdvisor.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<INewsService, NewsService>();
            container.RegisterType<IViewModelService,ViewModelService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}