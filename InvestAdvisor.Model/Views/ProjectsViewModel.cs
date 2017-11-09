using System.Collections.Generic;
using InvestAdvisor.Model.Views.Enums;

namespace InvestAdvisor.Model.Views
{
    public class ProjectsViewModel : BaseViewModel
    {
        public ProjectsViewModel(MenuItem activeMenuItem) : base(activeMenuItem)
        {
        }

        public List<ProjectModel> Projects { get; set; }
    }
}
