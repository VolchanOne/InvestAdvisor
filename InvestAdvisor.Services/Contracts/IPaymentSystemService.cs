using System.Collections.Generic;
using System.Threading.Tasks;
using InvestAdvisor.Model;

namespace InvestAdvisor.Services.Contracts
{
    public interface IPaymentSystemService
    {
        Task<List<PaymentSystemModel>> GetPaymentSystems();

        Task Create(PaymentSystemModel model);

        Task<PaymentSystemModel> FindById(int paymentSystemId);

        Task Update(PaymentSystemModel model);

        Task Delete(int paymentSystemId);

        Task AddImage(int paymentSystemId, ImageModel image);

        Task DeleteImage(int imageId);

        Task UpdateCurrencies(int paymentSystemId, int[] currencyIds);

        Task<PaymentSystemModel> FindByRoutePaymentSystemName(string routePaymentSystemName);
    }
}
