using Orchard;
using Orchard.Mvc;
using Orchard.Themes;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VirtoCommerce.WebStore.Converters;
using VirtoCommerce.WebStore.Services;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Controllers
{
    [Themed]
    public class CheckoutController : ControllerBase
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IShoppingCartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutController(IOrchardServices orchardServices, IShoppingCartService cartService, IOrderService orderService)
            : base(orchardServices, cartService)
        {
            _orchardServices = orchardServices;
            _cartService = cartService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var shape = _orchardServices.New.Checkout();

            var checkoutViewModel = await _cartService.GetCheckoutAsync(Settings.StoreId, CustomerId);

            shape.Checkout = checkoutViewModel;

            return new ShapeResult(this, shape);
        }

        [HttpGet]
        public async Task<ActionResult> SetShippingMethod(string shippingMethodId)
        {
            var checkoutViewModel = await _cartService.GetCheckoutAsync(Settings.StoreId, CustomerId);
            if (checkoutViewModel != null)
            {
                checkoutViewModel.ShippingMethodId = shippingMethodId;
            }

            return Json(checkoutViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Checkout checkout)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { errorMessage = GetModelErrorMessage(ModelState) });
            }

            var checkoutViewModel = await _cartService.GetCheckoutAsync(Settings.StoreId, CustomerId);

            checkoutViewModel.BillingAddress = checkout.ShippingAddress;
            checkoutViewModel.Email = checkout.Email;
            checkoutViewModel.PaymentMethodId = checkout.PaymentMethodId;
            checkoutViewModel.ShippingAddress = checkout.ShippingAddress;
            checkoutViewModel.ShippingMethodId = checkout.ShippingMethodId;

            await _cartService.UpdateCartAsync(checkoutViewModel);

            var orderViewModel = await _orderService.CreateOrderAsync(checkoutViewModel.Id);

            if (orderViewModel == null)
            {
                return Json(new { errorMessage = T("An error occured while order creating").ToString() });
            }

            await _cartService.DeleteCartAsync(checkoutViewModel.Id);

            var paymentResultViewModel = await _orderService.ProcessPaymentAsync(orderViewModel.Id, orderViewModel.PaymentId);

            if (paymentResultViewModel == null)
            {
                return Json(new { errorMessage = T("An error occured while payment processing").ToString() });
            }

            if (!paymentResultViewModel.IsSuccess)
            {
                return Json(new { errorMessage = paymentResultViewModel.Error });
            }

            if (paymentResultViewModel.PaymentType.Equals("Redirection", StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(paymentResultViewModel.RedirectUrl))
                {
                    return Json(new { redirectUrl = paymentResultViewModel.RedirectUrl });
                }
            }
            if (paymentResultViewModel.PaymentType.Equals("Unknown", StringComparison.OrdinalIgnoreCase))
            {
                return Json(new { redirectUrl = VirtualPathUtility.ToAbsolute(string.Format("~/Thanks?id={0}", orderViewModel.Id)) });
            }

            return Json(new { errorMessage = T("Unknown error occured").ToString() });
        }

        [HttpGet]
        public async Task<ActionResult> Thanks(string id)
        {
            var shape = _orchardServices.New.Thanks();

            var orderViewModel = await _orderService.GetOrderAsync(CustomerId, id);

            shape.Order = orderViewModel;

            return new ShapeResult(this, shape);
        }
    }
}