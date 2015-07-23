using Orchard;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using VirtoCommerce.Webshop.Models;
using VirtoCommerce.Webshop.Services;

namespace VirtoCommerce.Webshop.Drivers
{
    public class ShoppingCartPreviewDriver : ContentPartDriver<ShoppingCartPreviewPart>
    {
        private const string StoreId = "SampleStore";

        private readonly IOrchardServices _orchardServices;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartPreviewDriver(IOrchardServices orchardServices, IShoppingCartService shoppingCartService)
        {
            _orchardServices = orchardServices;
            _shoppingCartService = shoppingCartService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ShoppingCartPreviewPart part, string displayType, dynamic shapeHelper)
        {
            var currentUser = _orchardServices.WorkContext.CurrentUser;

            int lineItemCount = 0;

            var shoppingCart = _shoppingCartService.GetShoppingCartAsync(StoreId, currentUser.UserName).Result;
            if (shoppingCart != null)
            {
                lineItemCount = shoppingCart.LineItems.Count;
            }

            return ContentShape("Parts_ShoppingCartPreview", () => shapeHelper.Parts_ShoppingCartPreview(
                lineItemCount: lineItemCount));
        }
    }
}