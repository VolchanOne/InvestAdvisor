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
        private readonly INewsService _newsService;

        public ViewModelService(IProjectService projectService, INewsService newsService)
        {
            _projectService = projectService;
            _newsService = newsService;
        }

        public async Task<HomeViewModel> GetHomeModel()
        {
            var model = new HomeViewModel(MenuItem.Home);

            model.Projects = await _projectService.GetProjectsWithAdditional(true);

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
                News = await _newsService.GetNews()
            };

            return newsViewModel;
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
            var model = new ProjectsViewModel(MenuItem.Projects);
            model.ActiveSubMenuItem = isActive ? SubMenuItem.ProjectsActive : SubMenuItem.ProjectsClosed;

            model.Projects = await _projectService.GetProjectsWithAdditional(isActive, orderBy, orderDir);

            return model;
        }
    }
}
