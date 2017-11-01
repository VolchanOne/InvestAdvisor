using InvestAdvisor.Data.Contracts;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Data
{
    public class ProjectRepository : GenericRepository<InvestAdvisorDbContext, Project>, IProjectRepository
    {
    }
}
