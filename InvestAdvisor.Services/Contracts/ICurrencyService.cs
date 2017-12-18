using System.Collections.Generic;
using System.Threading.Tasks;
using InvestAdvisor.Model;

namespace InvestAdvisor.Services.Contracts
{
    public interface ICurrencyService
    {
        Task<List<CurrencyModel>> GetCurrencies();

        Task Create(CurrencyModel model);

        Task<CurrencyModel> FindById(int currencyId);

        Task Delete(int currencyId);
    }
}
