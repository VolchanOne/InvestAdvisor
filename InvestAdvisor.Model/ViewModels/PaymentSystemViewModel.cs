using InvestAdvisor.Model.ViewModels.Enums;

namespace InvestAdvisor.Model.ViewModels
{
    public class PaymentSystemViewModel : BaseViewModel
    {
        public PaymentSystemViewModel(MenuItem activeMenuItem) : base(activeMenuItem)
        {
        }

        public PaymentSystemModel PaymentSystem { get; set; }
    }
}
