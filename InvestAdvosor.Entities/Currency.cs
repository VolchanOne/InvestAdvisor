using System.Collections.Generic;
using InvestAdvisor.Common.Enums;

namespace InvestAdvosor.Entities
{
    public class Currency
    {
        public int CurrencyId { get; set; }

        public CurrencyAbbreviation Abbreviation { get; set; }

        public virtual List<PaymentSystem> PaymentSystems { get; set; }
    }
}
