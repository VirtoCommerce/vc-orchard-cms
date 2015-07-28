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
    public class ShoppingCartDriver : ContentPartDriver<ShoppingCartPart>
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartDriver(IOrchardServices orchardServices, IShoppingCartService shoppingCartService)
        {
            _orchardServices = orchardServices;
            _shoppingCartService = shoppingCartService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ShoppingCartPart part, string displayType, dynamic shapeHelper)
        {
            var settings = _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();

            string customerId = null;
            var anonymousCookie = HttpContext.Current.Request.Cookies[settings.AnonymousCookieId];
            if (anonymousCookie != null)
            {
                customerId = anonymousCookie.Value;
            }

            ShoppingCart shoppingCart = null;
            if (!string.IsNullOrEmpty(customerId))
            {
                shoppingCart = _shoppingCartService.GetShoppingCartAsync(settings.StoreId, customerId).Result;
            }

            if (shoppingCart == null || shoppingCart != null && shoppingCart.LineItemCount == 0)
            {
                return ContentShape("Parts_EmptyShoppingCart", () => shapeHelper.Parts_EmptyShoppingCart());
            }

            return ContentShape("Parts_ShoppingCart", () => shapeHelper.Parts_ShoppingCart(
                ShoppingCart: shoppingCart));
        }
    }
}