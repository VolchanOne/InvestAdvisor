using System.Threading.Tasks;
using InvestAdvisor.Model.ViewModels;
using InvestAdvisor.Model.ViewModels.Enums;
using InvestAdvisor.Services.Contracts;
using InvestAdvisor.Model;

namespace InvestAdvisor.Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly IProjectService _projectService;
        private readonly IPaymentSystemService _paymentSystemService;
        private readonly INewsService _newsService;

        public ViewModelService(IProjectService projectService, INewsService newsService, IPaymentSystemService paymentSystemService)
        {
            _projectService = projectService;
            _newsService = newsService;
            _paymentSystemService = paymentSystemService;
        }

        public async Task<HomeViewModel> GetHomeModel()
        {
            var model = new HomeViewModel(MenuItem.Home)
            {
                Projects = await _projectService.GetProjectsWithAdditional(true),
                News = await _newsService.GetNews(8)
            };

            return model;
        }

        public async Task<ProjectViewModel> GetProjectModel(int projectId)
        {
            var project = await _projectService.FindById(projectId);
            return GetProjectViewModel(project);
        }

        public async Task<NewsViewModel> GetNewsModel()
        {
            var newsViewModel = new NewsViewModel(MenuItem.News)
            {
                News = await _newsService.GetNews(20)
            };

            return newsViewModel;
        }

        public async Task<PaymentSystemsViewModel> GetPaymentSystemsModel()
        {
            var paymentSystemsViewModel = new PaymentSystemsViewModel(MenuItem.PaymentSystem)
            {
                PaymentSystems = await _paymentSystemService.GetPaymentSystems()
            };

            return paymentSystemsViewModel;
        }

        public async Task<PaymentSystemViewModel> GetPaymentSystemModel(string routePaymentSystemName)
        {
            var paymentSystem = await _paymentSystemService.FindByRoutePaymentSystemName(routePaymentSystemName);

            return new PaymentSystemViewModel(MenuItem.PaymentSystem)
            {
                PaymentSystem = paymentSystem
            };
        }

        private static ProjectViewModel GetProjectViewModel(ProjectModel project)
        {
            return new ProjectViewModel(MenuItem.Projects)
            {
                Project = project,
                ActiveSubMenuItem = project.IsActive ? SubMenuItem.ProjectsActive : SubMenuItem.ProjectsClosed
            };
        }

        public async Task<ProjectViewModel> GetProjectModel(string routeProjectName)
        {
            var project = await _projectService.FindByRouteProjectName(routeProjectName);

            return GetProjectViewModel(project);
        }

        public async Task<ProjectsViewModel> GetProjectsModel(bool isActive, string orderBy = null, string orderDir = null)
        {
            if (string.IsNullOrEmpty(orderDir))
                orderDir = "Desc";

            var model = new ProjectsViewModel(MenuItem.Projects)
            {
                ActiveSubMenuItem = isActive ? SubMenuItem.ProjectsActive : SubMenuItem.ProjectsClosed,
                Projects = await _projectService.GetProjectsWithAdditional(isActive, orderBy: orderBy, orderDir: orderDir)
            };

            return model;
        }
    }
}
