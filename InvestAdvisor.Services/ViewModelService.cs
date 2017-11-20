using System.Threading.Tasks;
using InvestAdvisor.Model.ViewModels;
using InvestAdvisor.Model.ViewModels.Enums;
using InvestAdvisor.Services.Contracts;
using System.Text.RegularExpressions;

namespace InvestAdvisor.Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly IProjectService _projectService;

        public ViewModelService(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<HomeViewModel> GetHomeModel()
        {
            var model = new HomeViewModel(MenuItem.Home);

            model.Projects = await _projectService.GetProjectsWithAdditional(true);

            return model;
        }

        public async Task<ProjectViewModel> GetProjectModel(string routeProjectName)
        {
            var project = await _projectService.FindByRouteProjectName(routeProjectName);

            return new ProjectViewModel(MenuItem.Projects)
            {
                Project = project,
                ActiveSubMenuItem = project.IsActive ? MenuItem.ProjectsActive : MenuItem.ProjectsClosed
            };
        }

        public async Task<ProjectsViewModel> GetProjectsModel(bool isActive, string orderBy = null, string orderDir = null)
        {
            var model = new ProjectsViewModel(MenuItem.Projects);
            model.ActiveSubMenuItem = isActive ? MenuItem.ProjectsActive : MenuItem.ProjectsClosed;

            model.Projects = await _projectService.GetProjectsWithAdditional(isActive, orderBy, orderDir);

            return model;
        }
    }
}
