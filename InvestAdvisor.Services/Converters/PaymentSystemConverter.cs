using System.Linq;
using InvestAdvisor.Model;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services.Converters
{
    public static class PaymentSystemConverter
    {
        public static PaymentSystemModel ToPaymentSystemModel(this PaymentSystem paymentSystem)
        {
            var paymentSystemModel = new PaymentSystemModel
            {
                PaymentSystemId = paymentSystem.PaymentSystemId,
                Name = paymentSystem.Name,
                ShortName = paymentSystem.ShortName,
                Url = paymentSystem.Url
            };
            if (paymentSystem.Images != null)
                paymentSystemModel.Images = paymentSystem.Images.Select(i => i.ToImageModel()).ToList();
            return paymentSystemModel;
        }
    }
}
