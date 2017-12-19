using System.Threading.Tasks;
using System.Web.Mvc;
using InvestAdvisor.Services.Contracts;

namespace InvestAdvisor.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IViewModelService _viewService;

        public ProjectsController(IViewModelService viewService)
        {
            _viewService = viewService;
        }

        public async Task<ActionResult> Index(string orderBy, string orderDir, string isActive)
        {
            var active = false;
            var model = await _viewService.GetProjectsModel(string.IsNullOrEmpty(isActive) || !bool.TryParse(isActive.ToLower(), out active) || active, orderBy,orderDir);
            ViewBag.IsList = true;

            return View(model);
        }        
    }
}