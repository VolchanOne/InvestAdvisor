using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
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
            var projectShorts = await _projectService.GetAll();

            return View(projectShorts);
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
            var projectModel = await _projectService.GetOne(id.Value);
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
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _projectService.GetOne(id.Value);
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
    }
}
