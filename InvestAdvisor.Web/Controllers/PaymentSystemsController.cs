using System.Threading.Tasks;
using System.Web.Mvc;
using InvestAdvisor.Services.Contracts;

namespace InvestAdvisor.Web.Controllers
{
    public class PaymentSystemsController : Controller
    {
        private readonly IViewModelService _viewService;

        public PaymentSystemsController(IViewModelService viewService)
        {
            _viewService = viewService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _viewService.GetPaymentSystemsModel();
            ViewBag.IsList = true;

            return View(model);
        }
    }
}