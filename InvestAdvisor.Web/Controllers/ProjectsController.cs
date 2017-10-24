using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvestAdvisor.Web.Models;
using InvestAdvisor.Web.Models.Enums;

namespace InvestAdvisor.Web.Controllers
{
    public class ProjectsController : Controller
    {
        public ActionResult All()
        {
            return View("ProjectsList",new BaseViewModel{ActiveMenuItem = MenuItem.Project});
        }
    }
}