using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using InvestAdvisor.Model.ViewModels;
using InvestAdvisor.Model.ViewModels.Enums;
using InvestAdvisor.Services.Contracts;

namespace InvestAdvisor.Web.Controllers
{
    public class PaymentSystemController : Controller
    {
        private readonly IViewModelService _viewService;

        public PaymentSystemController(IViewModelService viewService)
        {
            _viewService = viewService;
        }

        public async Task<ActionResult> Details([FromUri]string id, TabItem? activeTab = null)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.ActiveTabItem = activeTab;

            var model = await _viewService.GetPaymentSystemModel(id);
            return PaymentSystemView(model);
        }

        private ActionResult PaymentSystemView(PaymentSystemViewModel model)
        {
            ViewBag.IsList = false;
            return View(model);
        }
    }
}