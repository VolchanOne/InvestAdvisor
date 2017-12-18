using InvestAdvisor.Model;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services.Converters
{
    public static class CurrencyConverter
    {
        public static CurrencyModel ToCurrencyModel(this Currency currency)
        {
            return new CurrencyModel
            {
                CurrencyId = currency.CurrencyId,
                Abbreviation = currency.Abbreviation
            };
        }
    }
}
