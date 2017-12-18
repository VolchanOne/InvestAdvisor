using System.Collections.Generic;
using System.ComponentModel;

namespace InvestAdvisor.Model
{
    public class PaymentSystemModel
    {
        public PaymentSystemModel()
        {
            Images = new List<ImageModel>();
            Currencies = new List<CurrencyModel>();
        }
        public int PaymentSystemId { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Короткое название")]
        public string ShortName { get; set; }

        public List<ImageModel> Images { get; set; }

        public List<CurrencyModel> Currencies { get; set; }

        [DisplayName("Ссылка")]
        public string Url { get; set; }

        public string RouteName { get; set; }
    }
}
