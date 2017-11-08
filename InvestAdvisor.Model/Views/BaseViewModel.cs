using InvestAdvisor.Model.Views.Enums;

namespace InvestAdvisor.Model.Views
{
    public class BaseViewModel
    {
        public MenuItem ActiveMenuItem { get;  }

        public BaseViewModel(MenuItem activeMenuItem)
        {
            ActiveMenuItem = activeMenuItem;
        }
    }
}