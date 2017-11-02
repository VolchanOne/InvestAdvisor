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
                .GetWithInclude(p => p.ProjectId == projectId, p => p.Images)
                .FirstOrDefault();

            if (project == null)
                return null;

            return new ProjectModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                Url = project.Url,
                //IsPaymentSystem = project.IsPaymentSystem,
                //IsInvestment = project.IsFavorite,
                //Marketing = project.Marketing,
                //Referral = project.Referral,
                //StartDate = project.StartDate,
                //Invested = project.Invested,
                //Review = project.Review,
                //Domain = project.Domain,
                //Hosting = project.Hosting,
                //Ssl = project.Ssl,
                Images = project.Images.Select(i => new ImageModel
                {
                    ImageId = i.ImageId,
                    Name = i.Name,
                    ImageType = i.ImageType,
                    Content = i.Content
                }).ToList()
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
            //project.IsPaymentSystem = model.IsPaymentSystem;
            //project.IsFavorite = model.IsInvestment;
            //project.Marketing = model.Marketing;
            //project.Referral = model.Referral;
            //project.StartDate = model.StartDate;
            //project.Invested = model.Invested;
            //project.Review = model.Review;
            //project.Domain = model.Domain;
            //project.Hosting = model.Hosting;
            //project.Ssl = model.Ssl;

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
    }
}
