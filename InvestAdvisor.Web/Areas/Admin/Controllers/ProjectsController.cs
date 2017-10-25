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
                Name = p.Name
            }).ToListAsync());
        }

        // GET: Admin/Projects/Details/5
        public async Task<ActionResult> Details(int? id)
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
            project.Image = project.ImageId != null ? await _db.Images.FindAsync(project.ImageId) : null;

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
        public async Task<ActionResult> Create([Bind(Include = "ProjectId,Name")] ProjectViewModel createModel)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    ProjectId = createModel.ProjectId,
                    Name = createModel.Name,
                    CreatedAt = DateTime.Now
                };

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
            project.Image = project.ImageId != null ? await _db.Images.FindAsync(project.ImageId) : null;

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
                if (project == null)
                {
                    return HttpNotFound();
                }

                var imageUploaded = WebImage.GetImageFromRequest();
                if (imageUploaded != null)
                {
                    if (project.Image != null)
                    {
                        _db.Images.Remove(project.Image);
                    }

                    project.Image = new Image
                    {
                        Name = imageUploaded.FileName,
                        Content = imageUploaded.GetBytes()
                    };

                    _db.Images.Add(project.Image);
                }

                project.Name = editModel.Name;
                project.Description = editModel.Description;
                project.Url = editModel.Url;

                _db.Entry(project).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                editModel.ImageId = project.ImageId;
                editModel.Image = project.Image;
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
                Name = project.Name
            });
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            if(project.Image != null)
                _db.Images.Remove(project.Image);

            _db.Projects.Remove(project);
            
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
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
