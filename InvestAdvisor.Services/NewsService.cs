using System.Collections.Generic;
using System.Threading.Tasks;
using InvestAdvisor.Model;
using InvestAdvisor.Services.Contracts;
using InvestAdvisor.Data;
using System.Data.Entity;
using InvestAdvosor.Entities;
using System.Linq;
using System;
using InvestAdvisor.Services.Converters;

namespace InvestAdvisor.Services
{
    public class NewsService : INewsService
    {
        public async Task<List<NewsModel>> GetNews(int take)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var news = await db.News.OrderBy(n => n.PublishedAt).Take(take).ToListAsync();

                var newsModels = news.Select(n => n.ToNewsModel(true)).ToList();

                return newsModels;
            }
        }

        public async Task Create(NewsModel model)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                db.News.Add(new News
                {
                    NewsId = model.NewsId,
                    Title = model.Title
                });
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(NewsModel model)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var news = await db.News.FindAsync(model.NewsId);
                if (news == null)
                    return;

                news.NewsId = model.NewsId;
                news.Title = model.Title;
                news.Content = model.Content;
                if (!news.PublishedAt.HasValue)
                    news.PublishedAt = model.IsPublished.HasValue && model.IsPublished.Value ? DateTime.Now : default(DateTime?);

                if (model.SelectedProject != 0)
                {
                    var project = await db.Projects.FindAsync(model.SelectedProject);
                    if (project != null)
                    {
                        news.Project = project;
                        project.News.Add(news);
                    }
                }

                db.Entry(news).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<NewsModel> FindById(int newsId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var news = await db.News.FindAsync(newsId);
                if (news == null)
                    return null;
                var newsModel = news.ToNewsModel(true);

                return newsModel;
            }
        }

        public async Task Delete(int newsId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var news = await db.News.FindAsync(newsId);
                if (news == null)
                    return;

                db.News.Remove(news);
                await db.SaveChangesAsync();
            }
        }
    }
}
