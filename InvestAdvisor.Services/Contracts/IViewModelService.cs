using System.Threading.Tasks;
using InvestAdvisor.Model.Views;

namespace InvestAdvisor.Services.Contracts
{
    public interface IViewModelService
    {
        Task<HomeViewModel> GetHomeModel();

        Task<ProjectsViewModel> GetProjectsModel(bool isActive, string orderBy = null, string orderDir = null);

        Task<ProjectViewModel> GetProjectModel(string urlProjectName);
    }
}
