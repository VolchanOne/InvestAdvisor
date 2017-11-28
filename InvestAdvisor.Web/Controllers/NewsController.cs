using System.Threading.Tasks;
using System.Web.Mvc;
using InvestAdvisor.Services.Contracts;

namespace InvestAdvisor.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly IViewModelService _viewService;

        public NewsController(IViewModelService viewService)
        {
            _viewService = viewService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _viewService.GetNewsModel();
            ViewBag.IsList = true;

            return View(model);
        }
    }
}