using System.Collections.Generic;
using InvestAdvisor.Model.ViewModels.Enums;

namespace InvestAdvisor.Model.ViewModels
{
    public class ProjectsViewModel : BaseViewModel
    {
        public ProjectsViewModel(MenuItem activeMenuItem) : base(activeMenuItem)
        {
        }

        public List<ProjectModel> Projects { get; set; }
    }
}
