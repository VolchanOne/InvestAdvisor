using InvestAdvisor.Model;
using InvestAdvisor.Services.Contracts;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace InvestAdvisor.Web.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IProjectService _projectService;
        public NewsController(INewsService newsService, IProjectService projectService)
        {
            _newsService = newsService;
            _projectService = projectService;
        }

        public async Task<ActionResult> Index()
        {
            var news = await _newsService.GetNews();
            return View(news);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewsModel model)
        {
            if (ModelState.IsValid)
            {
                await _newsService.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var newsModel = await _newsService.FindById(id.Value);

            var projects = await _projectService.GetProjects();
            newsModel.ListProjects = projects.Select(p => new SelectListItem { Text = p.Name, Value = p.ProjectId.ToString() }).ToList();
            if (newsModel.Project != null)
                newsModel.SelectedProject = newsModel.Project.ProjectId;

            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NewsModel model)
        {
            if (ModelState.IsValid)
            {
                await _newsService.Update(model);
            }
            return RedirectToAction("Edit", new { id = model.NewsId });
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = await _newsService.FindById(id.Value);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _newsService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}