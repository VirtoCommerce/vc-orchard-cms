using Orchard;
using Orchard.Mvc;
using Orchard.Themes;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VirtoCommerce.WebStore.Services;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Controllers
{
    [Themed]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IStoreService _storeService;
        private readonly ICatalogService _catalogService;
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IOrchardServices orchardServices, IStoreService storeService, ICatalogService catalogService, IShoppingCartService cartService)
            : base(orchardServices, cartService)
        {
            _orchardServices = orchardServices;
            _storeService = storeService;
            _catalogService = catalogService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var shape = _orchardServices.New.Cart();

            var cartViewModel = await _cartService.GetCartAsync(Settings.StoreId, CustomerId);

            if (cartViewModel == null || cartViewModel != null && cartViewModel.LineItemsCount == 0)
            {
                shape = _orchardServices.New.EmptyCart();
            }

            shape.ShoppingCart = cartViewModel;

            return new ShapeResult(this, shape);
        }

        [HttpGet]
        public async Task<ActionResult> AddItem(string slug)
        {
            var storeViewModel = await _storeService.GetStoreAsync(Settings.StoreId);

            var productViewModel = await _catalogService.GetProductAsync(Settings.StoreId, Settings.Culture, slug);

            if (ShoppingCart == null)
            {
                await _cartService.CreateCartAsync(Settings.StoreId, CustomerId);

                ShoppingCart = await _cartService.GetCartAsync(Settings.StoreId, CustomerId);
            }

            var lineItem = ShoppingCart.LineItems.FirstOrDefault(li => li.ProductId == productViewModel.Id);
            if (lineItem == null)
            {
                ShoppingCart.LineItems.Add(new LineItem
                {
                    CatalogId = storeViewModel.CatalogId,
                    CategoryId = "fake",
                    Image = productViewModel.FeaturedImage,
                    Price = productViewModel.Price,
                    ProductId = productViewModel.Id,
                    Quantity = 1,
                    Sku = productViewModel.Sku,
                    Title = productViewModel.Title
                });
            }
            else
            {
                lineItem.Quantity++;
            }

            await _cartService.UpdateCartAsync(ShoppingCart);

            ShoppingCart = await _cartService.GetCartAsync(Settings.StoreId, CustomerId);

            return Json(ShoppingCart, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateLineItem(string lineItemId, int quantity)
        {
            if (quantity <= 0)
            {
                return Json(new { errorMessage = T("Quantity must be positive number").ToString() });
            }

            var lineItemViewModel = ShoppingCart.LineItems.FirstOrDefault(li => li.Id == lineItemId);
            if (lineItemViewModel != null)
            {
                lineItemViewModel.Quantity = quantity;
            }

            await _cartService.UpdateCartAsync(ShoppingCart);

            ShoppingCart = await _cartService.GetCartAsync(Settings.StoreId, CustomerId);

            return Json(ShoppingCart, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteLineItem(string lineItemId)
        {
            var lineItemViewModel = ShoppingCart.LineItems.FirstOrDefault(li => li.Id == lineItemId);

            ShoppingCart.LineItems.Remove(lineItemViewModel);

            await _cartService.UpdateCartAsync(ShoppingCart);

            ShoppingCart = await _cartService.GetCartAsync(Settings.StoreId, CustomerId);

            return Json(ShoppingCart, JsonRequestBehavior.AllowGet);
        }
    }
}