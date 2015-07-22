using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System;
using System.Linq;
using System.Web;
using VirtoCommerce.Webshop.Models;
using VirtoCommerce.Webshop.Services;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Drivers
{
    public class ProductListDriver : ContentPartDriver<ProductListPart>
    {
        private const string StoreId = "SampleStore";
        private const string Culture = "en-US";
        private const string CatalogId = "VendorVirtual";
        private const string Currency = "USD";
        private const int PageSize = 20;

        private readonly ICatalogService _catalogService;
        private readonly IPriceService _priceService;

        public ProductListDriver(ICatalogService catalogService, IPriceService priceService)
        {
            _catalogService = catalogService;
            _priceService = priceService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ProductListPart part, string displayType, dynamic shapeHelper)
        {
            var httpRequest = HttpContext.Current.Request;

            string categorySlug = null;
            int page = 1;
            if (httpRequest.Url.Segments.Any(s => s.Equals("Category", StringComparison.OrdinalIgnoreCase)))
            {
                categorySlug = httpRequest.QueryString["id"];

                if (httpRequest.QueryString["p"] != null)
                {
                    int.TryParse(httpRequest.QueryString["p"], out page);
                }
            }

            PagedList<Product> productPagedList = null;

            if (!String.IsNullOrEmpty(categorySlug))
            {
                var pricelists = _priceService.GetPricelistsAsync(CatalogId, Currency).Result;

                productPagedList = _catalogService.SearchProductsAsync(StoreId, Culture, categorySlug, (page - 1) * PageSize, PageSize, pricelists).Result;
            }

            return ContentShape("Parts_ProductList", () => shapeHelper.Parts_ProductList(
                SelectedCategorySlug: categorySlug,
                Products: productPagedList));
        }

    }
}