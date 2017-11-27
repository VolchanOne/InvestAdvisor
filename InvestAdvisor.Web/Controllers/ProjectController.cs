using InvestAdvisor.Model;
using InvestAdvisor.Model.ViewModels;
using InvestAdvisor.Model.ViewModels.Enums;
using InvestAdvisor.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace InvestAdvisor.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IViewModelService _viewService;
        private readonly IProjectService _projectService;

        public ProjectController(IViewModelService viewService, IProjectService projectService)
        {
            _viewService = viewService;
            _projectService = projectService;
        }

        public async Task<ActionResult> Details([FromUri]string id, TabItem? activeTab = null)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.ActiveTabItem = activeTab;

            var model = await _viewService.GetProjectModel(id);
            return ProjectView(model);
        }

        private ActionResult ProjectView(ProjectViewModel model)
        {
            ViewBag.IsList = false;
            return View(model);
        }

        public async Task<ActionResult> AddComment(int projectId, CommentModel comment)
        {
            await _projectService.AddComment(projectId, comment);
            var model = await _viewService.GetProjectModel(projectId);

            return RedirectToAction("Details", new { id = model.Project.RouteName, activeTab = TabItem.Comments });
        }
    }
}