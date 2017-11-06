using System;
using System.Threading.Tasks;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using InvestAdvisor.Common.Enums;
using InvestAdvisor.Model;
using InvestAdvisor.Services.Contracts;

namespace InvestAdvisor.Web.Areas.Admin.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: Admin/Projects
        public async Task<ActionResult> Index()
        {
            var projects = await _projectService.GetAll();

            return View(projects);
        }

        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectModel = await _projectService.FindById(id.Value);
            if (projectModel == null)
            {
                return HttpNotFound();
            }
            return View(projectModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProjectModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Update(model);
            }
            return RedirectToAction("Edit", new {id = model.ProjectId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAdditional(int projectId, ProjectAdditionalModel additional)
        {
            if (ModelState.IsValid)
            {
                await _projectService.UpdateAdditional(projectId, additional);
            }
            return RedirectToAction("Edit", new {id = projectId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditReview(int projectId, ProjectReviewModel review)
        {
            if (ModelState.IsValid)
            {
                await _projectService.UpdateReview(projectId, review);
            }
            return RedirectToAction("Edit", new { id = projectId });
        }

        // GET: Admin/Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _projectService.FindById(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _projectService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddImage(int projectId, int? imageType)
        {
            var imageUploaded = WebImage.GetImageFromRequest();
            if (imageUploaded != null && imageType.HasValue && Enum.IsDefined(typeof(ImageType), imageType))
            {
                await _projectService.AddImage(projectId, new ImageModel
                {
                    Name = imageUploaded.FileName,
                    Content = imageUploaded.GetBytes(),
                    ImageType = (ImageType) imageType
                });
            }
            return RedirectToAction("Edit", new {id = projectId});
        }

        public async Task<ActionResult> DeleteImage(int projectId, int imageId)
        {
            await _projectService.DeleteImage(imageId);
            return RedirectToAction("Edit", new {id = projectId});
        }
    }
}