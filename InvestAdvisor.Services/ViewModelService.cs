using System.Threading.Tasks;
using InvestAdvisor.Model.Views;
using InvestAdvisor.Model.Views.Enums;
using InvestAdvisor.Services.Contracts;

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

            model.Projects = await _projectService.GetActiveProjectsWithAdditional();

            return model;
        }

        public async Task<ProjectsViewModel> GetProjectsModel(string orderBy = null, string orderDir = null)
        {
            var model = new ProjectsViewModel(MenuItem.Projects);
            model.ActiveSubMenuItem = MenuItem.ProjectsActive;

            model.Projects = await _projectService.GetActiveProjectsWithAdditional(orderBy, orderDir);

            return model;
        }
    }
}
