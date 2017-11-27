using InvestAdvisor.Model.ViewModels.Enums;

namespace InvestAdvisor.Model.ViewModels
{
    public class BaseViewModel
    {
        public MenuItem ActiveMenuItem { get;  }

        public SubMenuItem? ActiveSubMenuItem { get; set; }

        public BaseViewModel(MenuItem activeMenuItem)
        {
            ActiveMenuItem = activeMenuItem;
        }
    }
}