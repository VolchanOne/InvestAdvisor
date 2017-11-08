using System.Collections.Generic;
using System.Threading.Tasks;
using InvestAdvisor.Model;

namespace InvestAdvisor.Services.Contracts
{
    public interface IProjectService
    {
        Task<List<ProjectModel>> GetAll();

        Task<List<ProjectModel>> GetAllWithAdditional();

        Task<ProjectModel> FindById(int projectId);

        Task Create(ProjectModel model);

        Task Update(ProjectModel model);

        Task Delete(int projectId);

        Task UpdateAdditional(int projectId, ProjectAdditionalModel model);

        Task UpdateReview(int projectId, ProjectReviewModel review);

        Task UpdateActivity(int projectId, bool? inPortfolio, bool? isActive);

        Task AddImage(int projectId, ImageModel image);

        Task DeleteImage(int imageId);
    }
}
