using System.Linq;
using InvestAdvisor.Model;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services.Converters
{
    public static class PaymentSystemConverter
    {
        public static PaymentSystemModel ToPaymentSystemModel(this PaymentSystem paymentSystem, bool allInfo)
        {
            var paymentSystemModel = new PaymentSystemModel
            {
                PaymentSystemId = paymentSystem.PaymentSystemId,
                Name = paymentSystem.Name,
                ShortName = paymentSystem.ShortName,
                RouteName = paymentSystem.RouteName,
                Url = paymentSystem.Url
            };
            if (paymentSystem.Images != null)
                paymentSystemModel.Images = paymentSystem.Images.Select(i => i.ToImageModel()).ToList();
            if (allInfo)
            {
                if (paymentSystem.Currencies != null)
                    paymentSystemModel.Currencies = paymentSystem.Currencies.Select(i => i.ToCurrencyModel()).ToList();
                if (paymentSystem.Review != null)
                    paymentSystemModel.Review = new ProjectReviewModel
                    {
                        ProjectReviewId = paymentSystem.Review.ProjectReviewId,
                        Review = paymentSystem.Review.Review
                    };
            }
            return paymentSystemModel;
        }
    }
}
