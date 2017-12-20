using System.Linq;
using InvestAdvisor.Model;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services.Converters
{
    public static class ProjectConverter
    {
        public static ProjectModel ToProjectModel(this Project project)
        {
            return project.ToProjectModel(false, false);
        }

        public static ProjectModel ToProjectModel(this Project project, bool withAditional)
        {
            return project.ToProjectModel(true, false);
        }

        public static ProjectModel ToProjectModel(this Project project, bool withAdditional, bool allInfo)
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
                projectModel.Images = project.Images.Select(i => i.ToImageModel()).ToList();
            if (withAdditional && project.Additional != null)
                projectModel.Additional = new ProjectAdditionalModel
                {
                    ProjectAdditionalId = project.Additional.ProjectAdditionalId,
                    Marketing = project.Additional.Marketing,
                    Referral = project.Additional.Referral,
                    StartDate = project.Additional.StartDate?.ToString("yyyy-MM-dd")
                };
            if (allInfo)
            {
                if (project.Review != null)
                    projectModel.Review = new ProjectReviewModel
                    {
                        ProjectReviewId = project.Review.ProjectReviewId,
                        Review = project.Review.Review
                    };
                if (project.TechInfo != null)
                    projectModel.TechInfo = new ProjectTechModel
                    {
                        Domain = project.TechInfo.Domain,
                        Hosting = project.TechInfo.Hosting,
                        Ssl = project.TechInfo.Ssl
                    };
                if (project.Comments != null)
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
                if (project.PaymentSystems != null)
                {
                    projectModel.PaymentSystems = project.PaymentSystems.Select(p => p.ToPaymentSystemModel(false)).ToList();
                }
            }
            return projectModel;
        }
    }
}
