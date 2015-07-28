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
    public class ShoppingCartPreviewDriver : ContentPartDriver<ShoppingCartPreviewPart>
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartPreviewDriver(IOrchardServices orchardServices, IShoppingCartService shoppingCartservice)
        {
            _orchardServices = orchardServices;
            _shoppingCartService = shoppingCartservice;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ShoppingCartPreviewPart part, string displayType, dynamic shapeHelper)
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

            return ContentShape("Parts_ShoppingCartPreview", () => shapeHelper.Parts_ShoppingCartPreview(
                ShoppingCart: shoppingCart));
        }
    }
}