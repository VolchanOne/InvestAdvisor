using InvestAdvisor.Model;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services.Converters
{
    public static class NewsConverter
    {
        public static NewsModel ToNewsModel(this News news)
        {
            return news.ToNewsModel(false);
        }

        public static NewsModel ToNewsModel(this News news, bool withProject)
        {
            var newsModel = new NewsModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                Content = news.Content,
                IsPublished = news.PublishedAt.HasValue
            };
            if (withProject && news.Project != null)
            {
                newsModel.Project = news.Project.ToProjectModel();
            }
            return newsModel;
        }
    }
}
