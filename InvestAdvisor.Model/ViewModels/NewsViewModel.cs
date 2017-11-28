using InvestAdvisor.Model.ViewModels.Enums;
using System.Collections.Generic;

namespace InvestAdvisor.Model.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public NewsViewModel(MenuItem activeMenuItem) : base(activeMenuItem)
        {
        }

        public List<NewsModel> News { get; set; }
    }
}
