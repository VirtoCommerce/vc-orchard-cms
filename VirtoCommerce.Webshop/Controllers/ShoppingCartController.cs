using Orchard;
using Orchard.Localization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VirtoCommerce.Webshop.Services;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string StoreId = "SampleStore";
        private const string Currency = "USD";
        private const string Culrture = "en-US";
        private const string Pricelist = "SaleUSD";

        private readonly IOrchardServices _orchardServices;
        private readonly ICatalogService _catalogService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IOrchardServices orchardServices, ICatalogService catalogService, IShoppingCartService shoppingCartService)
        {
            _orchardServices = orchardServices;
            _catalogService = catalogService;
            _shoppingCartService = shoppingCartService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        // POST: /ShoppingCart/Add
        [HttpPost]
        public async Task<JsonResult> Add(string sku, int? quantity)
        {
            var productModel = await _catalogService.GetProductBySkuAsync(StoreId, Culrture, Pricelist, sku);
            if (productModel == null)
            {
                return Json(new { errorMessage = T("Product was not found").Text });
            }

            var customer = _orchardServices.WorkContext.CurrentUser;
            if (customer == null)
            {
                return Json(new { errorMessage = T("Customer was not found").Text });
            }

            var shoppingCartModel = await _shoppingCartService.GetShoppingCartAsync(StoreId, customer.UserName);
            if (shoppingCartModel == null)
            {
                shoppingCartModel = new ShoppingCart()
                {
                    Currency = Currency,
                    CustomerId = customer.UserName,
                    Name = "default",
                    StoreId = StoreId
                };

                await _shoppingCartService.CreateShoppingCartAsync(shoppingCartModel);
                shoppingCartModel = await _shoppingCartService.GetShoppingCartAsync(StoreId, customer.UserName);
            }

            quantity = quantity ?? 1;

            shoppingCartModel.Add(productModel, (int)quantity);

            await _shoppingCartService.UpdateShoppingCartAsync(shoppingCartModel);

            return Json(new { shoppingCartModel.LineItems.Count });
        }
    }
}