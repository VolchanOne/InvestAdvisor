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

            model.Projects = await _projectService.GetProjectsWithAdditional();

            return model;
        }
    }
}
