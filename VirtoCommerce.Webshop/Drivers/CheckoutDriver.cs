using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System.Web;
using VirtoCommerce.Webshop.Models;
using VirtoCommerce.Webshop.Services;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Drivers
{
    public class CheckoutDriver : ContentPartDriver<CheckoutPart>
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IShoppingCartService _shoppingCartService;

        public CheckoutDriver(IOrchardServices orchardServices, IShoppingCartService shoppingCartservice)
        {
            _orchardServices = orchardServices;
            _shoppingCartService = shoppingCartservice;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(CheckoutPart part, string displayType, dynamic shapeHelper)
        {
            var settings = _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();

            string customerId = null;
            var anonymousCookie = HttpContext.Current.Request.Cookies[settings.AnonymousCookieId];
            if (anonymousCookie != null)
            {
                customerId = anonymousCookie.Value;
            }

            Checkout checkoutModel = null;
            if (!string.IsNullOrEmpty(customerId))
            {
                checkoutModel = _shoppingCartService.GetCheckoutAsync(settings.StoreId, customerId).Result;
            }

            return ContentShape("Parts_Checkout", () => shapeHelper.Parts_Checkout(
                Checkout: checkoutModel));
        }
    }
}