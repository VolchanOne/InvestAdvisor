using System.Threading.Tasks;
using InvestAdvisor.Model.Views;

namespace InvestAdvisor.Services.Contracts
{
    public interface IViewModelService
    {
        Task<HomeViewModel> GetHomeModel();

        Task<ProjectsViewModel> GetProjectsModel(string orderBy = null, string orderDir = null);
    }
}
