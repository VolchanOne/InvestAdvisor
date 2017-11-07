using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using InvestAdvisor.Data.Contracts;
using InvestAdvisor.Model;
using InvestAdvisor.Services.Contracts;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IImageRepository _imageRepository;

        public ProjectService(IProjectRepository projectRepository, IImageRepository imageRepository)
        {
            _projectRepository = projectRepository;
            _imageRepository = imageRepository;
        }

        public async Task<List<ProjectModel>> GetAll()
        {
            var projectModels = await _projectRepository.Get().Select(p => new ProjectModel
            {
                ProjectId = p.ProjectId,
                Name = p.Name,
                Description = p.Description,
                Url = p.Url
            }).ToListAsync();
            
            return projectModels;
        }

        public async Task<ProjectModel> FindById(int projectId)
        {
            var project = await _projectRepository.FindByIdAsync(projectId);

            if (project == null)
                return null;

            var projectModel = new ProjectModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                Url = project.Url,
                IsActive = project.IsActive,
                InPortofolio = project.InPortofolio
            };
            if (project.Images != null)
                projectModel.Images = project.Images.Select(i => new ImageModel
                {
                    ImageId = i.ImageId,
                    Name = i.Name,
                    ImageType = i.ImageType,
                    Content = i.Content
                }).ToList();
            if (project.Additional != null)
                projectModel.Additional = new ProjectAdditionalModel
                {
                    ProjectAdditionalId = project.Additional.ProjectAdditionalId,
                    Marketing = project.Additional.Marketing,
                    Referral = project.Additional.Referral,
                    StartDate = project.Additional.StartDate?.ToString("yyyy-MM-dd")
                };
            if (project.Review != null)
                projectModel.Review = new ProjectReviewModel
                {
                    ProjectReviewId = project.Review.ProjectReviewId,
                    Review = project.Review.Review
                };

            return projectModel;
        }

        public async Task Create(ProjectModel model)
        {
            _projectRepository.Create(new Project
            {
                ProjectId = model.ProjectId,
                Name = model.Name
            });

            await _projectRepository.SaveChangesAsync();
        }

        public async Task Update(ProjectModel model)
        {
            var project = await _projectRepository.FindByIdAsync(model.ProjectId);

            if (project == null)
                return;

            project.ProjectId = model.ProjectId;
            project.Name = model.Name;
            project.Description = model.Description;
            project.Url = model.Url;

            _projectRepository.Update(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task UpdateAdditional(int projectId, ProjectAdditionalModel model)
        {
            var project = await _projectRepository.FindByIdAsync(projectId);

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

            _projectRepository.Update(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task UpdateReview(int projectId, ProjectReviewModel model)
        {
            var project = await _projectRepository.FindByIdAsync(projectId);

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

            _projectRepository.Update(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task UpdateActivity(int projectId, bool inPortfolio, bool isActive)
        {
            var project = await _projectRepository.FindByIdAsync(projectId);

            if (project == null)
                return;

            project.InPortofolio = inPortfolio;
            project.IsActive = isActive;

            _projectRepository.Update(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task Delete(int projectId)
        {
            var project = await _projectRepository.FindByIdAsync(projectId);

            if (project == null)
                return;

            project.Images.ForEach(i => _imageRepository.Remove(i));

            _projectRepository.Remove(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task AddImage(int projectId, ImageModel image)
        {
            var project = await _projectRepository.FindByIdAsync(projectId);

            if (project == null)
                return;

            project.Images.Add(new Image
            {
                Name = image.Name,
                Content = image.Content,
                ImageType = image.ImageType
            });
            _projectRepository.Update(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task DeleteImage(int imageId)
        {
            var image = await _imageRepository.FindByIdAsync(imageId);
            if (image == null)
                return;
            _imageRepository.Remove(image);
            await _imageRepository.SaveChangesAsync();
        }
    }
}
