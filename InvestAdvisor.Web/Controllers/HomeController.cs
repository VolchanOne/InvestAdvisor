using System.Web.Mvc;
using InvestAdvisor.Web.Models;
using InvestAdvisor.Web.Models.Enums;

namespace InvestAdvisor.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new BaseViewModel
            {
                ActiveMenuItem = MenuItem.Home
            });
        }
    }
}