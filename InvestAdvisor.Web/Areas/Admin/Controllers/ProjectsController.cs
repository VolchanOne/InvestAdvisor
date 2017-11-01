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
            var projects = _db.Projects;
            return View(await projects.Select(p => new ProjectViewModel
            {
                ProjectId = p.ProjectId,
                Name = p.Name
            }).ToListAsync());
        }

        // GET: Admin/Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            return await Edit(id, true);
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
        public async Task<ActionResult> Edit(int? id, bool? isDetails = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _db.Projects.Include(p => p.Images).Include(p => p.PaymentSystems).FirstOrDefaultAsync(p => p.ProjectId == id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsEdit = !isDetails ?? true;
            return View("EditDetails", new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                Url = project.Url,
                IsPaymentSystem = project.IsPaymentSystem,
                IsInvestment = project.IsFavorite,
                Marketing = project.Marketing,
                Referral = project.Referral,
                StartDate = project.StartDate,
                Invested = project.Invested,
                Review = project.Review,
                Domain = project.Domain,
                Hosting = project.Hosting,
                Ssl = project.Ssl,
                Images = project.Images,
                PaymentSystems = project.PaymentSystems
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProjectId,Name,Description,Url,IsPaymentSystem")] ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = await _db.Projects.FindAsync(model.ProjectId);
                if (project == null)
                {
                    return HttpNotFound();
                }

                project.ProjectId = model.ProjectId;
                project.Name = model.Name;
                project.Description = model.Description;
                project.Url = model.Url;
                project.IsPaymentSystem = model.IsPaymentSystem;
                project.IsFavorite = model.IsInvestment;
                project.Marketing = model.Marketing;
                project.Referral = model.Referral;
                project.StartDate = model.StartDate;
                project.Invested = model.Invested;
                project.Review = model.Review;
                project.Domain = model.Domain;
                project.Hosting = model.Hosting;
                project.Ssl = model.Ssl;

                _db.Entry(project).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                model.Images = project.Images;
                model.PaymentSystems = project.PaymentSystems;
                return RedirectToAction("Index");
            }
            ViewBag.IsEdit = true;
            return View("EditDetails", model);
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

            project.Images.ToList().ForEach(i => _db.Images.Remove(i));

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
