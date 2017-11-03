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
            var projects = await _projectRepository.Get().ToListAsync();
            var projectModels = projects.Select(p => new ProjectModel
            {
                ProjectId = p.ProjectId,
                Name = p.Name,
                Description = p.Description,
                Url = p.Url
            }).ToList();
            return projectModels;
        }

        public async Task<ProjectModel> FindById(int projectId)
        {
            var project = _projectRepository
                .GetWithInclude(p => p.ProjectId == projectId, p => p.Images, p => p.Additional)
                .FirstOrDefault();

            if (project == null)
                return null;

            return new ProjectModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                Url = project.Url,
                Images = project.Images.Select(i => new ImageModel
                {
                    ImageId = i.ImageId,
                    Name = i.Name,
                    ImageType = i.ImageType,
                    Content = i.Content
                }).ToList(),
                Additional = new ProjectAdditionalModel
                {
                    ProjectAdditionalId = project.Additional.ProjectAdditionalId,
                    Legend = project.Additional.Legend,
                    Marketing = project.Additional.Marketing,
                    Referral = project.Additional.Referral,
                    StartDate = project.Additional.StartDate?.ToString("yyyy-MM-dd")
                }
            };
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

            var additional = new ProjectAdditional
            {
                Legend = model.Legend,
                Marketing = model.Marketing,
                Referral = model.Referral,
                StartDate = !string.IsNullOrEmpty(model.StartDate) ? DateTime.Parse(model.StartDate) : default(DateTime?) 
            };

            project.Additional = additional;

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
