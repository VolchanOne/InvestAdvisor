using System.Threading.Tasks;
using System.Web.Mvc;
using InvestAdvisor.Services.Contracts;

namespace InvestAdvisor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelService _viewService;

        public HomeController(IViewModelService viewService)
        {
            _viewService = viewService;
        }

        public async Task <ActionResult> Index()
        {
            var model = await _viewService.GetHomeModel();

            return View(model);
        }
    }
}