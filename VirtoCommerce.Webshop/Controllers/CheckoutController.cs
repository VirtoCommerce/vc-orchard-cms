using Orchard;
using Orchard.Localization;
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

            return Json(new { errorMessage = T("Some payment error was occured!").Text });
        }

        // POST: /Checkout/SetShippingMethod
        [HttpPost]
        public async Task<JsonResult> SetShippingMethod(Address address, string shippingMethodId)
        {
            if (string.IsNullOrEmpty(shippingMethodId))
            {
                return Json(new { errorMessage = T("You did not set shipping method").Text });
            }

            if (!ModelState.IsValid)
            {
                return Json(new { errorMessage = GetModelErrorMessage(ModelState) });
            }

            var customerId = GetCustomerId(HttpContext, Settings.AnonymousCookieId);

            var checkout = await _shoppingCartService.GetCheckoutAsync(Settings.StoreId, customerId);
            var shippingMethods = await _shoppingCartService.GetShippingMethodsAsync(checkout.Id);

            var shippingMethod = shippingMethods.FirstOrDefault(sm => sm.Keyword.Equals(shippingMethodId, StringComparison.OrdinalIgnoreCase));
            if (shippingMethod == null)
            {
                return Json(new { errorMessage = T("Selected shipping method was not found").Text });
            }

            checkout.ShippingAddress = address;
            checkout.ShippingMethodId = shippingMethodId;

            await _shoppingCartService.UpdateCheckoutAsync(checkout);

            checkout = await _shoppingCartService.GetCheckoutAsync(Settings.StoreId, customerId);

            return Json(new { checkout = checkout });
        }

        // POST: /Checkout/SetPaymentMethod/
        [HttpPost]
        public async Task<JsonResult> SetPaymentMethod(Address address, string paymentMethodId)
        {
            if (string.IsNullOrEmpty(paymentMethodId))
            {
                return Json(new { errorMessage = T("You did not set payment method").Text });
            }

            if (!ModelState.IsValid)
            {
                return Json(new { errorMessage = GetModelErrorMessage(ModelState) });
            }

            var customerId = GetCustomerId(HttpContext, Settings.AnonymousCookieId);

            var checkout = await _shoppingCartService.GetCheckoutAsync(Settings.StoreId, customerId);
            var paymentMethods = await _shoppingCartService.GetPaymentMethodsAsync(checkout.Id);

            var paymentMethod = paymentMethods.FirstOrDefault(sm => sm.Keyword.Equals(paymentMethodId, StringComparison.OrdinalIgnoreCase));
            if (paymentMethod == null)
            {
                return Json(new { errorMessage = T("Selected payment method was not found").Text });
            }

            if (paymentMethod.Type.Equals("PreparedForm", StringComparison.OrdinalIgnoreCase) ||
                paymentMethod.Type.Equals("Stardard", StringComparison.OrdinalIgnoreCase))
            {
                return Json(new { errorMessage = T(string.Format("{0} payment method is not supported for this demo", paymentMethod.Name)).Text });
            }

            checkout.BillingAddress = address;
            checkout.PaymentMethodId = paymentMethodId;

            await _shoppingCartService.UpdateCheckoutAsync(checkout);

            checkout = await _shoppingCartService.GetCheckoutAsync(Settings.StoreId, customerId);

            return Json(new { checkout = checkout });
        }

        // POST: /Checkout/CreateOrder/
        [HttpPost]
        public async Task<JsonResult> CreateOrder()
        {
            var customerId = GetCustomerId(HttpContext, Settings.AnonymousCookieId);

            var checkout = await _shoppingCartService.GetCheckoutAsync(Settings.StoreId, customerId);
            var checkoutError = checkout.GetError();
            if (!string.IsNullOrEmpty(checkoutError))
            {
                return Json(new { errorMessage = checkoutError });
            }

            var orderModel = await _orderService.CreateOrderAsync(checkout.Id);
            if (orderModel == null)
            {
                return Json(new { errorMessage = T("An error occured while order creating").Text });
            }

            await _shoppingCartService.DeleteShoppingCartAsync(checkout.Id);

            var paymentResult = await _orderService.ProcessPaymentAsync(orderModel.Id, orderModel.PaymentId);
            if (paymentResult == null)
            {
                return Json(new { errorMessage = T("An error occured while payment processing").Text });
            }

            if (!paymentResult.IsSuccess)
            {
                return Json(new { errorMessage = paymentResult.Error });
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

            return Json(new { errorMessage = T("Unknown error occured").Text });
        }

        private string GetModelErrorMessage(ModelStateDictionary modelState)
        {
            string errorMessage = T("Some unknown error").Text;

            var errorValues = modelState.Values.Where(v => v.Errors.Count > 0);
            if (errorValues != null)
            {
                var errorValue = errorValues.FirstOrDefault();
                if (errorValue != null)
                {
                    var error = errorValue.Errors.FirstOrDefault();
                    if (error != null)
                    {
                        errorMessage = error.ErrorMessage;
                    }
                }
            }

            return errorMessage;
        }
    }
}