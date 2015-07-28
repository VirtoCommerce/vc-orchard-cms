using Orchard;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VirtoCommerce.Webshop.Converters;
using VirtoCommerce.Webshop.Services;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Controllers
{
    public class ShoppingCartController : ControllerBase
    {
        private readonly IOrchardServices _orchardServices;
        private readonly ICatalogService _catalogService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IOrchardServices orchardServices, ICatalogService catalogService, IShoppingCartService shoppingCartService)
            : base(orchardServices)
        {
            _orchardServices = orchardServices;
            _catalogService = catalogService;
            _shoppingCartService = shoppingCartService;
        }

        // POST: /ShoppingCart/Add
        [HttpPost]
        public async Task<JsonResult> Add(string slug, int? q)
        {
            int quantity = q ?? 1;

            var customerId = GetCustomerId(HttpContext, Settings.AnonymousCookieId);

            var productModel = await _catalogService.GetProductAsync(Settings.StoreId, Settings.Culture, slug);

            if (productModel == null)
            {
                return Json(new { errorMessage = T("Product not found").Text });
            }

            var shoppingCartModel = await _shoppingCartService.GetShoppingCartAsync(Settings.StoreId, customerId);

            var shopModels = await _catalogService.GetShopsAsync();
            var shopModel = shopModels.FirstOrDefault(s => s.Id == Settings.StoreId);

            if (shoppingCartModel == null)
            {
                shoppingCartModel = new ShoppingCart();
                shoppingCartModel.Culture = Settings.Culture;
                shoppingCartModel.Currency = shopModel.Currency;
                shoppingCartModel.CustomerId = customerId;
                shoppingCartModel.StoreId = shopModel.Id;

                await _shoppingCartService.CreateShoppingCartAsync(shoppingCartModel);

                shoppingCartModel = await _shoppingCartService.GetShoppingCartAsync(Settings.StoreId, customerId);
            }

            shoppingCartModel.Add(productModel.ToLineItem(quantity, shopModel.Currency));

            await _shoppingCartService.UpdateShoppingCartAsync(shoppingCartModel, null);

            return Json(new { shoppingCart = shoppingCartModel });
        }

        // POST: /ShoppingCart/Change/
        [HttpPost]
        public async Task<JsonResult> Change(string lineItemId, int quantity)
        {
            if (quantity <= 0)
            {
                return Json(new { errorMessage = @T("Quantity must have positive value").Text });
            }

            var customerId = GetCustomerId(HttpContext, Settings.AnonymousCookieId);

            var shoppingCartModel = await _shoppingCartService.GetShoppingCartAsync(Settings.StoreId, customerId);
            if (shoppingCartModel == null)
            {
                return Json(new { errorMessage = @T("Shopping cart was not found").Text });
            }

            var lineItemModel = shoppingCartModel.LineItems.FirstOrDefault(li => li.Id == lineItemId);
            if (lineItemModel == null)
            {
                return Json(new { errorMessage = @T("Line item was not found").Text });
            }

            lineItemModel.Quantity = quantity;

            await _shoppingCartService.UpdateShoppingCartAsync(shoppingCartModel, null);

            return Json(new { shoppingCart = shoppingCartModel });
        }

        // POST: /ShoppingCart/Remove/
        [HttpPost]
        public async Task<JsonResult> Remove(string lineItemId)
        {
            var customerId = GetCustomerId(HttpContext, Settings.AnonymousCookieId);

            var shoppingCartModel = await _shoppingCartService.GetShoppingCartAsync(Settings.StoreId, customerId);
            if (shoppingCartModel == null)
            {
                return Json(new { errorMessage = @T("Shopping cart was not found").Text });
            }

            shoppingCartModel.Remove(lineItemId);

            await _shoppingCartService.UpdateShoppingCartAsync(shoppingCartModel, null);

            return Json(new { shoppingCart = shoppingCartModel });
        }
    }
}