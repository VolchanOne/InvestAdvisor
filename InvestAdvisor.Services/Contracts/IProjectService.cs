using System.Collections.Generic;
using System.Threading.Tasks;
using InvestAdvisor.Model;

namespace InvestAdvisor.Services.Contracts
{
    public interface IProjectService
    {
        Task<List<ProjectModel>> GetProjects();

        Task<List<ProjectModel>> GetProjectsWithAdditional(bool isActive, string orderBy = null, string orderDir = null);

        Task<ProjectModel> FindById(int projectId);

        Task<ProjectModel> FindByRouteProjectName(string urlProjectName);

        Task Create(ProjectModel model);

        Task Update(ProjectModel model);

        Task Delete(int projectId);

        Task UpdateAdditional(int projectId, ProjectAdditionalModel model);

        Task UpdateReview(int projectId, ProjectReviewModel review);

        Task UpdateTechInfo(int projectId, ProjectTechModel techModel);

        Task UpdateActivity(int projectId, bool? inPortfolio, bool? isActive);

        Task AddImage(int projectId, ImageModel image);

        Task DeleteImage(int imageId);

        Task DeleteComment(int commentId);

        Task AddComment(int projectId, CommentModel model);

        Task UpdateComment(CommentModel comment);
    }
}
