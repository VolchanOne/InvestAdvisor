using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using InvestAdvisor.Data;
using InvestAdvisor.Model;
using InvestAdvisor.Services.Contracts;
using InvestAdvisor.Services.Converters;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services
{
    public class CurrencyService : ICurrencyService
    {
        public async Task<List<CurrencyModel>> GetCurrencies()
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var currencies = await db.Currencies.ToListAsync();

                var currencyModels = currencies.Select(p => p.ToCurrencyModel()).ToList();

                return currencyModels;
            }
        }

        public async Task Create(CurrencyModel model)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                db.Currencies.Add(new Currency
                {
                    CurrencyId = model.CurrencyId,
                    Abbreviation = model.Abbreviation
                });

                await db.SaveChangesAsync();
            }
        }

        public async Task<CurrencyModel> FindById(int currencyId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var currency = await db.Currencies.FindAsync(currencyId);
                var currencyModel = currency?.ToCurrencyModel();

                return currencyModel;
            }
        }

        public async Task Delete(int currencyId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var currency = await db.Currencies.FindAsync(currencyId);
                if (currency == null)
                    return;

                db.Currencies.Remove(currency);
                await db.SaveChangesAsync();
            }
        }
    }
}
