using InvestAdvisor.Model.Views.Enums;

namespace InvestAdvisor.Model.Views
{
    public class ProjectViewModel :BaseViewModel
    {
        public ProjectViewModel(MenuItem activeMenuItem) : base(activeMenuItem)
        {
        }

        public ProjectModel Project { get; set; }    
    }
}
