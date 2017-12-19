using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using InvestAdvisor.Common.Extensions;
using InvestAdvisor.Data;
using InvestAdvisor.Model;
using InvestAdvisor.Services.Contracts;
using InvestAdvisor.Services.Converters;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services
{
    public class PaymentSystemService : IPaymentSystemService
    {
        public async Task<List<PaymentSystemModel>> GetPaymentSystems()
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var paySystems = await db.PaymentSystems.ToListAsync();

                var paySystemModels = paySystems.Select(p => p.ToPaymentSystemModel()).ToList();

                return paySystemModels;
            }
        }

        public async Task Create(PaymentSystemModel model)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                db.PaymentSystems.Add(new PaymentSystem
                {
                    PaymentSystemId = model.PaymentSystemId,
                    Name = model.Name,
                    RouteName = model.Name.RemoveNonAlphaNumericChars()
                });
                await db.SaveChangesAsync();
            }
        }

        public async Task<PaymentSystemModel> FindById(int paymentSystemId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var paymentSystem = await db.PaymentSystems.FindAsync(paymentSystemId);
                var paymentSystemModel = paymentSystem?.ToPaymentSystemModel();

                return paymentSystemModel;
            }
        }

        public async Task Update(PaymentSystemModel model)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var paymentSystem = await db.PaymentSystems.FindAsync(model.PaymentSystemId);
                if (paymentSystem == null)
                    return;

                paymentSystem.PaymentSystemId = model.PaymentSystemId;
                paymentSystem.Name = model.Name;
                paymentSystem.RouteName = model.Name.RemoveNonAlphaNumericChars();
                paymentSystem.ShortName = model.ShortName;
                paymentSystem.Url = model.Url;

                db.Entry(paymentSystem).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(int paymentSystemId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var paymentSystem = await db.PaymentSystems.FindAsync(paymentSystemId);
                if (paymentSystem == null)
                    return;

                if (paymentSystem.Images != null)
                {
                    for (var i = paymentSystem.Images.Count - 1; i >= 0; i--)
                    {
                        db.Images.Remove(paymentSystem.Images[i]);
                    }
                }

                db.PaymentSystems.Remove(paymentSystem);
                await db.SaveChangesAsync();
            }
        }

        public async Task AddImage(int paymentSystemId, ImageModel image)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var paymentSystem = await db.PaymentSystems.FindAsync(paymentSystemId);
                if (paymentSystem == null)
                    return;

                paymentSystem.Images.Add(new Image
                {
                    Name = image.Name,
                    Content = image.Content,
                    ImageType = image.ImageType
                });

                db.Entry(paymentSystem).State = EntityState.Modified;

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteImage(int imageId)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var image = await db.Images.FindAsync(imageId);
                if (image == null)
                    return;

                db.Images.Remove(image);

                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateCurrencies(int paymentSystemId, int[] currencyIds)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                if (currencyIds == null || currencyIds.Length == 0)
                    return;

                var paymentSystem = await db.PaymentSystems.FindAsync(paymentSystemId);
                if (paymentSystem == null)
                    return;

                paymentSystem.Currencies.Clear();

                foreach (var currencyId in currencyIds)
                {
                    var currency = await db.Currencies.FindAsync(currencyId);
                    if (currency != null)
                        paymentSystem.Currencies.Add(currency);
                }

                db.Entry(paymentSystem).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<PaymentSystemModel> FindByRoutePaymentSystemName(string routePaymentSystemName)
        {
            using (var db = new InvestAdvisorDbContext())
            {
                var paymentSystem = await db.PaymentSystems.FirstOrDefaultAsync(p => p.RouteName == routePaymentSystemName);
                var paymentSystemModel = paymentSystem?.ToPaymentSystemModel();

                return paymentSystemModel;
            }
        }
    }
}
