using System.Linq;
using InvestAdvisor.Model;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services.Converters
{
    public static class ProjectConverter
    {
        public static ProjectModel ToProjectModel(this Project project)
        {
            return project.ToProjectModel(false);
        }

        public static ProjectModel ToProjectModel(this Project project, bool withAdditional)
        {
            return project.ToProjectModel(withAdditional, false);
        }

        public static ProjectModel ToProjectModel(this Project project, bool withAdditional, bool withReview)
        {
            return project.ToProjectModel(withAdditional, withReview, false);
        }

        public static ProjectModel ToProjectModel(this Project project, bool withAdditional, bool withReview, bool withTech)
        {
            return project.ToProjectModel(withAdditional, withReview, withTech, false);
        }

        public static ProjectModel ToProjectModel(this Project project, bool withAdditional, bool withReview, bool withTech, bool withComments)
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
            if (withComments && project.Comments != null)
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
    }
}
