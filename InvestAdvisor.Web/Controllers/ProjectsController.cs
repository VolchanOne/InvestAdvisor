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

        public async Task<ActionResult> All(string orderBy, string orderDir)
        {
            var model = await _viewService.GetProjectsModel(orderBy,orderDir);

            return View(model);
        }

        public ActionResult GetProjectDetails()
        {
            return null;
        }
    }
}