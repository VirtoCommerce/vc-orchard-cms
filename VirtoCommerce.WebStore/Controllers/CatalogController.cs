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
    public class CatalogController : ControllerBase
    {
        private readonly IOrchardServices _orchardServices;
        private readonly ICatalogService _catalogService;

        public CatalogController(IOrchardServices orchardServices, ICatalogService catalogService, IShoppingCartService cartService)
            : base(orchardServices, cartService)
        {
            _orchardServices = orchardServices;
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<ActionResult> Category(string slug, int? page)
        {
            page = page ?? 1;

            var shape = _orchardServices.New.Category();

            var categoryViewModels = await _catalogService.GetCategoriesAsync(Settings.StoreId, Settings.Culture);

            var productViewModels = await _catalogService.GetProductsAsync(Settings.StoreId, Settings.Culture, slug, (page.Value - 1) * Settings.PageSize, Settings.PageSize);

            Category currentCategoryViewModel = null;
            if (!string.IsNullOrEmpty(slug))
            {
                currentCategoryViewModel = categoryViewModels.FirstOrDefault(c => c.Slug == slug);
            }

            shape.Category = currentCategoryViewModel;
            shape.CategoryList = categoryViewModels;
            shape.ProductList = productViewModels;
            shape.ShoppingCart = ShoppingCart;

            return new ShapeResult(this, shape);
        }

        [HttpGet]
        public async Task<ActionResult> Product(string slug)
        {
            var shape = _orchardServices.New.Product();

            var categoryViewModels = await _catalogService.GetCategoriesAsync(Settings.StoreId, Settings.Culture);
            var productViewModel = await _catalogService.GetProductAsync(Settings.StoreId, Settings.Culture, slug);

            shape.Product = productViewModel;
            shape.CategoryList = categoryViewModels;
            shape.ShoppingCart = ShoppingCart;

            return new ShapeResult(this, shape);
        }
    }
}