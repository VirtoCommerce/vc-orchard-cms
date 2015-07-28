using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VirtoCommerce.Webshop.Services;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Controllers
{
    public class CheckoutController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;

        public CheckoutController(IOrchardServices orchardServices, IShoppingCartService shoppingCartService, IOrderService orderService)
            : base(orchardServices)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }

        // GET: /Checkout/ExternalPaymentCallback
        [HttpGet]
        public async Task<JsonResult> ExternalPaymentCallback()
        {
            var parameters = new List<KeyValuePair<string, string>>();

            foreach (var key in HttpContext.Request.QueryString.AllKeys)
            {
                parameters.Add(new KeyValuePair<string, string>(key, HttpContext.Request.QueryString[key]));
            }

            var paymentResult = await _orderService.ProcessPaymentAsync(parameters);
            if (paymentResult != null)
            {
                if (paymentResult.IsSuccess)
                {
                    string orderId = HttpContext.Request.QueryString["orderId"];

                    return Json(new { redirectUrl = VirtualPathUtility.ToAbsolute("~/Thanks") });
                }

                return Json(new { errorMessage = paymentResult.Error });
            }

            return Json(new { errorMessage = "Some payment error was occured!" });
        }

        // POST: /Order/Create
        [HttpPost]
        public async Task<JsonResult> Create(Checkout checkout)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(f => f.Value.Errors.Count > 0);
                var errorMessage = errors.Select(f => f.Value.Errors.First().ErrorMessage);

                return Json(new { errorMessage = errorMessage });
            }

            var customerId = GetCustomerId(HttpContext, Settings.AnonymousCookieId);

            var shoppingCartModel = await _shoppingCartService.GetShoppingCartAsync(Settings.StoreId, customerId);

            await _shoppingCartService.UpdateShoppingCartAsync(shoppingCartModel, checkout);

            var orderModel = await _orderService.CreateOrderAsync(shoppingCartModel.Id);
            if (orderModel == null)
            {
                return Json(new { errorMessage = T("An error occured while order creating").Text });
            }

            await _shoppingCartService.DeleteShoppingCartAsync(shoppingCartModel.Id);

            var paymentResult = await _orderService.ProcessPaymentAsync(orderModel.Id, orderModel.PaymentId);
            if (paymentResult == null)
            {
                return Json(new { errorMessage = T("An error occured while payment processing").Text });
            }

            if (!paymentResult.IsSuccess)
            {
                return Json(new { errorMessage = paymentResult.Error });
            }

            if (paymentResult.PaymentType.Equals("PreparedForm", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(paymentResult.HtmlForm))
                {
                    return Json(new { htmlForm = paymentResult.HtmlForm });
                }
            }
            if (paymentResult.PaymentType.Equals("Redirection", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(paymentResult.RedirectUrl))
                {
                    return Json(new { redirectUrl = paymentResult.RedirectUrl });
                }
            }
            if (paymentResult.PaymentType.Equals("Unknown", StringComparison.OrdinalIgnoreCase))
            {
                return Json(new { redirectUrl = VirtualPathUtility.ToAbsolute(string.Format("~/Thanks?id={0}", orderModel.Id)) });
            }

            return Json(new { errorMessage = "Unknown error occured" });
        }
    }
}