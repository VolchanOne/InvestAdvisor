using InvestAdvisor.Common.Enums;

namespace InvestAdvosor.Entities
{
    public class Currency
    {
        public int CurrencyId { get; set; }

        public CurrencyAbbreviation Abbreviation { get; set; }
    }
}
