using System.Collections.Generic;
using InvestAdvisor.Model.ViewModels.Enums;

namespace InvestAdvisor.Model.ViewModels
{
    public class PaymentSystemsViewModel : BaseViewModel
    {
        public PaymentSystemsViewModel(MenuItem activeMenuItem) : base(activeMenuItem)
        {
        }

        public List<PaymentSystemModel> PaymentSystems { get; set; }
    }
}
