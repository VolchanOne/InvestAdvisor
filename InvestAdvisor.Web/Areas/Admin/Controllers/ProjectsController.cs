using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using InvestAdvisor.Data;
using InvestAdvisor.Model;
using InvestAdvisor.Web.Areas.Admin.Models;

namespace InvestAdvisor.Web.Areas.Admin.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly InvestAdvisorDbContext _db = new InvestAdvisorDbContext();

        // GET: Admin/Projects
        public async Task<ActionResult> Index()
        {
            var projects = _db.Projects.Include(p => p.Image);
            return View(await projects.Select(p => new ProjectViewModel
            {
                ProjectId = p.ProjectId,
                Name = p.Name,
                Description = p.Description,
                Url = p.Url,
                ImageId = p.ImageId,
                Image = p.Image
            }).ToListAsync());
        }

        // GET: Admin/Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await _db.Projects.FindAsync(id);

            if (project == null)
            {
                return HttpNotFound();
            }
            project.Image = await _db.Images.FindAsync(project.ImageId);

            return View(new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                Url = project.Url,
                ImageId = project.ImageId,
                Image = project.Image
            });
        }

        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProjectId,Name,Description,Url")] ProjectViewModel createModel)
        {
            var imageUploaded = WebImage.GetImageFromRequest();
            if (imageUploaded == null)
                ModelState.AddModelError("Image", "The Image is required");

            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    ProjectId = createModel.ProjectId,
                    Name = createModel.Name,
                    Description = createModel.Description,
                    Url = createModel.Url
                };

                AddImage(project, imageUploaded);
                project.CreatedAt = DateTime.Now;

                _db.Projects.Add(project);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(createModel);
        }

        // GET: Admin/Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _db.Projects.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            project.Image = await _db.Images.FindAsync(project.ImageId);
            return View(new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                Url = project.Url,
                ImageId = project.ImageId,
                Image = project.Image
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProjectId,Name,Description,Url,ImageId")] ProjectViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                var project = await _db.Projects.FindAsync(editModel.ProjectId);

                var imageUploaded = WebImage.GetImageFromRequest();
                if (imageUploaded != null)
                {
                     AddImage(project, imageUploaded);

                    var image = await _db.Images.FindAsync(editModel.ImageId);
                    _db.Images.Remove(image);
                }

                editModel.Image = project.Image;
                project.Name = editModel.Name;
                project.Description = editModel.Description;
                project.Url = editModel.Url;
                _db.Entry(project).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(editModel);
        }

        // GET: Admin/Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await _db.Projects.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                Url = project.Url,
                ImageId = project.ImageId,
                Image = project.Image
            });
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            var image = await _db.Images.FindAsync(project.ImageId);
            _db.Projects.Remove(project);
            _db.Images.Remove(image);

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private void AddImage(Project project, WebImage imageUploaded)
        {
            var image = new Image
            {
                Name = imageUploaded.FileName,
                Content = imageUploaded.GetBytes()
            };

            _db.Images.Add(image);

            project.Image = image;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
