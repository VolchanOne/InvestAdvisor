using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using InvestAdvisor.Common.Enums;
using InvestAdvisor.Model;
using InvestAdvisor.Services.Contracts;

namespace InvestAdvisor.Web.Areas.Admin.Controllers
{
    public class PaymentSystemsController : Controller
    {
        private readonly IPaymentSystemService _paymentSystemService;

        public PaymentSystemsController(IPaymentSystemService paymentSystemService)
        {
            _paymentSystemService = paymentSystemService;
        }

        public async Task<ActionResult> Index()
        {
            var paySystems = await _paymentSystemService.GetPaymentSystems();
            return View(paySystems);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentSystemModel model)
        {
            if (ModelState.IsValid)
            {
                await _paymentSystemService.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var paySystemModel = await _paymentSystemService.FindById(id.Value);
            if (paySystemModel == null)
            {
                return HttpNotFound();
            }
            return View(paySystemModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PaymentSystemModel model)
        {
            if (ModelState.IsValid)
            {
                await _paymentSystemService.Update(model);
            }
            return RedirectToAction("Edit", new { id = model.PaymentSystemId });
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _paymentSystemService.FindById(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _paymentSystemService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddImage(int paymentSystemId, int? imageType)
        {
            var imageUploaded = WebImage.GetImageFromRequest();
            if (imageUploaded != null && imageType.HasValue && Enum.IsDefined(typeof(ImageType), imageType))
            {
                await _paymentSystemService.AddImage(paymentSystemId, new ImageModel
                {
                    Name = imageUploaded.FileName,
                    Content = imageUploaded.GetBytes(),
                    ImageType = (ImageType)imageType
                });
            }
            return RedirectToAction("Edit", new { id = paymentSystemId });
        }

        public async Task<ActionResult> DeleteImage(int paymentSystemId, int imageId)
        {
            await _paymentSystemService.DeleteImage(imageId);
            return RedirectToAction("Edit", new { id = paymentSystemId });
        }
    }
}