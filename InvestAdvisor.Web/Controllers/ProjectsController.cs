using System.Web.Mvc;
using InvestAdvisor.Model.Views;
using InvestAdvisor.Model.Views.Enums;

namespace InvestAdvisor.Web.Controllers
{
    public class ProjectsController : Controller
    {
        public ActionResult All(string orderBy)
        {
            return View(new BaseViewModel(MenuItem.Project));
        }

        public ActionResult GetProjectDetails()
        {
            return null;
        }
    }
}