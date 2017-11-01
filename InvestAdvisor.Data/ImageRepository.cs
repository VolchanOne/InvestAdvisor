using InvestAdvisor.Data.Contracts;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Data
{
    public class ImageRepository : GenericRepository<InvestAdvisorDbContext, Image>, IImageRepository
    {
    }
}
