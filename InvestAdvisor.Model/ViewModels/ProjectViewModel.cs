using InvestAdvisor.Model.ViewModels.Enums;

namespace InvestAdvisor.Model.ViewModels
{
    public class ProjectViewModel :BaseViewModel
    {
        public ProjectViewModel(MenuItem activeMenuItem) : base(activeMenuItem)
        {
        }

        public ProjectModel Project { get; set; }    
    }
}
