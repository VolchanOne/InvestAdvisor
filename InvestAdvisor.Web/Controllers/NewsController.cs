using InvestAdvisor.Model.ViewModels;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace InvestAdvisor.Web.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
    }
}