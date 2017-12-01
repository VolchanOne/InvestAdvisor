using InvestAdvisor.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestAdvisor.Services.Contracts
{
    public interface INewsService
    {
        Task<List<NewsModel>> GetNews(int take);

        Task Create(NewsModel model);

        Task Update(NewsModel model);

        Task<NewsModel> FindById(int newsId);

        Task Delete(int newsId);
    }
}
