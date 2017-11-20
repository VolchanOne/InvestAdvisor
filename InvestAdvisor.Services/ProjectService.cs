using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using InvestAdvisor.Data;
using InvestAdvisor.Model;
using InvestAdvisor.Services.Contracts;
using InvestAdvosor.Entities;
using System.Text.RegularExpressions;
using InvestAdvisor.Common.Extensions;

namespace InvestAdvisor.Services
{
    public class ProjectService : IProjectService
    {
        public async Task<List<ProjectModel>> GetProjects()
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var projects = await db.Projects.ToListAsync();

                var projectModels = projects.Select(p => ProjectToProjectModel(p, false, false, false, false)).ToList();

                return projectModels;
            }
        }

        public async Task<List<ProjectModel>> GetProjectsWithAdditional(bool isActive, string orderBy = null, string orderDir = null)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var projects = await db.Projects.ToListAsync();

                var projectModels = projects.Where(p => (isActive ? p.ActivatedAt.HasValue : !p.ActivatedAt.HasValue)).Select(p => ProjectToProjectModel(p, true, false, false, false)).ToList();
                projectModels = orderDir == "Desc" ? projectModels.OrderByDescending(KeySelector(orderBy)).ToList() : projectModels.OrderBy(KeySelector(orderBy)).ToList();

                return projectModels;
            }
        }

        private static Func<ProjectModel, object> KeySelector(string orderBy)
        {
            switch (orderBy)
            {
                case "StartDate": return p => p.Additional.StartDate;
                default: return p => p.ActivatedAt;
            }
        }

        public async Task<ProjectModel> FindById(int projectId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var project = await db.Projects.FindAsync(projectId);
                if (project == null)
                    return null;
                var projectModel = ProjectToProjectModel(project, true, true, true, true);

                return projectModel;
            }
        }

        public async Task<ProjectModel> FindByRouteProjectName(string routeProjectName)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var project = await db.Projects.FirstOrDefaultAsync(p => p.RouteName == routeProjectName);
                if (project == null)
                    return null;
                var projectModel = ProjectToProjectModel(project, true, true, true, true);

                return projectModel;
            }
        }

        public async Task Create(ProjectModel model)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                db.Projects.Add(new Project
                {
                    ProjectId = model.ProjectId,
                    Name = model.Name,
                    RouteName = model.Name.RemoveNonAlphaNumericChars()
                });
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(ProjectModel model)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var project = await db.Projects.FindAsync(model.ProjectId);
                if (project == null)
                    return;

                project.ProjectId = model.ProjectId;
                project.Name = model.Name;
                project.RouteName = model.Name.RemoveNonAlphaNumericChars();
                project.Description = model.Description;
                project.Url = model.Url;

                db.Entry(project).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateAdditional(int projectId, ProjectAdditionalModel model)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var project = await db.Projects.FindAsync(projectId);
                if (project == null)
                    return;

                if (project.Additional == null)
                {
                    project.Additional = new ProjectAdditional
                    {
                        Marketing = model.Marketing,
                        Referral = model.Referral,
                        StartDate = !string.IsNullOrEmpty(model.StartDate)
                            ? DateTime.Parse(model.StartDate)
                            : default(DateTime?)
                    };
                }
                else
                {
                    project.Additional.Marketing = model.Marketing;
                    project.Additional.Referral = model.Referral;
                    project.Additional.StartDate = !string.IsNullOrEmpty(model.StartDate)
                        ? DateTime.Parse(model.StartDate)
                        : default(DateTime?);
                }

                db.Entry(project).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateReview(int projectId, ProjectReviewModel model)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var project = await db.Projects.FindAsync(projectId);
                if (project == null)
                    return;

                if (project.Review == null)
                {
                    project.Review = new ProjectReview
                    {
                        Review = model.Review
                    };
                }
                else
                {
                    project.Review.Review = model.Review;
                }

                db.Entry(project).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateTechInfo(int projectId, ProjectTechModel techModel)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var project = await db.Projects.FindAsync(projectId);
                if (project == null)
                    return;

                if (project.TechInfo == null)
                {
                    project.TechInfo = new ProjectTech
                    {
                        Domain = techModel.Domain,
                        Hosting = techModel.Hosting,
                        Ssl = techModel.Ssl
                    };
                }
                else
                {
                    project.TechInfo.Domain = techModel.Domain;
                    project.TechInfo.Hosting = techModel.Hosting;
                    project.TechInfo.Ssl = techModel.Ssl;
                }

                db.Entry(project).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateActivity(int projectId, bool? inPortfolio, bool? isActive)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var project = await db.Projects.FindAsync(projectId);
                if (project == null)
                    return;

                if (inPortfolio.HasValue)
                    project.InPortofolio = inPortfolio.Value;
                if (isActive.HasValue)
                {
                    if (isActive.Value && !project.ActivatedAt.HasValue)
                        project.ActivatedAt = DateTime.Now;
                    project.IsActive = isActive.Value;
                }

                db.Entry(project).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(int projectId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var project = await db.Projects.FindAsync(projectId);
                if (project == null)
                    return;
                if (project.Additional != null)
                    db.ProjectAdditionals.Remove(project.Additional);
                if (project.Review != null)
                    db.ProjectReviews.Remove(project.Review);
                if (project.TechInfo != null)
                    db.ProjectTechs.Remove(project.TechInfo);

                if (project.Images != null)
                {
                    for (var i = project.Images.Count - 1; i >= 0; i--)
                    {
                        db.Images.Remove(project.Images[i]);
                    }
                }

                if (project.News != null)
                {
                    for (var i = project.News.Count - 1; i >= 0; i--)
                    {
                        db.News.Remove(project.News[i]);
                    }
                }

                db.Projects.Remove(project);
                await db.SaveChangesAsync();
            }
        }

        public async Task AddImage(int projectId, ImageModel image)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var project = await db.Projects.FindAsync(projectId);
                if (project == null)
                    return;

                project.Images.Add(new Image
                {
                    Name = image.Name,
                    Content = image.Content,
                    ImageType = image.ImageType
                });

                db.Entry(project).State = EntityState.Modified;

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteImage(int imageId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var image = await db.Images.FindAsync(imageId);
                if (image == null)
                    return;

                db.Images.Remove(image);

                await db.SaveChangesAsync();
            }
        }

        private static ProjectModel ProjectToProjectModel(Project project, bool withAdditional, bool withReview, bool withTech, bool withComments)
        {
            var projectModel = new ProjectModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                Url = project.Url,
                IsActive = project.IsActive,
                ActivatedAt = project.ActivatedAt,
                InPortofolio = project.InPortofolio,
                RouteName = project.RouteName
            };
            if (project.Images != null)
                projectModel.Images = project.Images.Select(i => new ImageModel
                {
                    ImageId = i.ImageId,
                    Name = i.Name,
                    ImageType = i.ImageType,
                    Content = i.Content
                }).ToList();
            if (withAdditional && project.Additional != null)
                projectModel.Additional = new ProjectAdditionalModel
                {
                    ProjectAdditionalId = project.Additional.ProjectAdditionalId,
                    Marketing = project.Additional.Marketing,
                    Referral = project.Additional.Referral,
                    StartDate = project.Additional.StartDate?.ToString("yyyy-MM-dd")
                };
            if (withReview && project.Review != null)
                projectModel.Review = new ProjectReviewModel
                {
                    ProjectReviewId = project.Review.ProjectReviewId,
                    Review = project.Review.Review
                };
            if (withTech && project.TechInfo != null)
                projectModel.TechInfo = new ProjectTechModel
                {
                    Domain = project.TechInfo.Domain,
                    Hosting = project.TechInfo.Hosting,
                    Ssl = project.TechInfo.Ssl
                };
            if(withComments && project.Comments != null)
            {
                projectModel.Comments = project.Comments.Select(c => new CommentModel
                {
                    CommentId = c.CommentId,
                    UserName = c.UserName,
                    Email = c.Email,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt
                }).ToList();
            }
            return projectModel;
        }

        public async Task AddComment(int projectId, CommentModel model)
        {
            try
            {
                using (var db = new InvestAdvisorDbContext())
                {
                    var project = await db.Projects.FindAsync(projectId);
                    if (project == null)
                        return;

                    project.Comments.Add(new Comment
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Message = model.Message,
                        CreatedAt = DateTime.Now
                    });

                    db.Entry(project).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
                         
        }
    }
}
