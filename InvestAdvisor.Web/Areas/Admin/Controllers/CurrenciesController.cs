using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using InvestAdvisor.Common.Enums;
using InvestAdvisor.Model;
using InvestAdvisor.Services.Contracts;

namespace InvestAdvisor.Web.Areas.Admin.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public async Task<ActionResult> Index()
        {
            var currencies = await _currencyService.GetCurrencies();

            return View(currencies);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: Admin/Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int? currencyAbbreviation)
        {
            var currencies = await _currencyService.GetCurrencies();

            if (currencyAbbreviation.HasValue && Enum.IsDefined(typeof(CurrencyAbbreviation), currencyAbbreviation) && currencies.All(a => a.Abbreviation != (CurrencyAbbreviation)currencyAbbreviation.Value))
            {
                await _currencyService.Create(new CurrencyModel
                {
                    Abbreviation = (CurrencyAbbreviation)currencyAbbreviation
                });
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currency = await _currencyService.FindById(id.Value);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _currencyService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}