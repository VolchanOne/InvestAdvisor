using System.Collections.Generic;
using InvestAdvisor.Model.ViewModels.Enums;

namespace InvestAdvisor.Model.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(MenuItem activeMenuItem) : base(activeMenuItem)
        {
        }

        public List<ProjectModel> Projects { get; set; }

        public List<NewsModel> News { get; set; }
    }
}
