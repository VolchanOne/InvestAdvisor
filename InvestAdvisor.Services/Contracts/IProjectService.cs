using System.Collections.Generic;
using System.Threading.Tasks;
using InvestAdvisor.Model;

namespace InvestAdvisor.Services.Contracts
{
    public interface IProjectService
    {
        Task<List<ProjectModel>> GetAll();

        Task<ProjectModel> FindById(int projectId);

        Task Create(ProjectModel model);

        Task Update(ProjectModel model);

        Task Delete(int projectId);

        Task UpdateAdditional(int projectId, ProjectAdditionalModel model);

        Task AddImage(int projectId, ImageModel image);

        Task DeleteImage(int imageId);
    }
}
