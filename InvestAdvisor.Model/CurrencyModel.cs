using System.ComponentModel;
using InvestAdvisor.Common.Enums;

namespace InvestAdvisor.Model
{
    public class CurrencyModel
    {
        public int CurrencyId { get; set; }

        [DisplayName("Аббревиатура")]
        public CurrencyAbbreviation Abbreviation { get; set; }
    }
}
