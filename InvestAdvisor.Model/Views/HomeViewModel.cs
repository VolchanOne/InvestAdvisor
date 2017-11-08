using System.Collections.Generic;
using InvestAdvisor.Model.Views.Enums;

namespace InvestAdvisor.Model.Views
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(MenuItem activeMenuItem) : base(activeMenuItem)
        {
        }

        public List<ProjectModel> Projects { get; set; }
    }
}
