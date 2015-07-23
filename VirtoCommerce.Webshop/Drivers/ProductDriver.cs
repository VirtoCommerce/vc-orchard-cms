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
    public class ProductDriver : ContentPartDriver<ProductPart>
    {
        private const string StoreId = "SampleStore";
        private const string Culture = "en-US";
        private const string CatalogId = "VendorVirtual";
        private const string Currency = "USD";

        private readonly ICatalogService _catalogService;
        private readonly IPriceService _priceService;

        public ProductDriver(ICatalogService catalogService, IPriceService priceService)
        {
            _catalogService = catalogService;
            _priceService = priceService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ProductPart part, string displayType, dynamic shapeHelper)
        {
            var httpRequest = HttpContext.Current.Request;

            string productSlug = null;
            if (httpRequest.Url.Segments.Any(s => s.Equals("Product", StringComparison.OrdinalIgnoreCase)))
            {
                productSlug = httpRequest.QueryString["slug"];
            }

            var pricelists = _priceService.GetPricelistsAsync(CatalogId, Currency).Result;

            var productModel = _catalogService.GetProductBySlugAsync(StoreId, Culture, pricelists.First(), productSlug).Result;

            return ContentShape("Parts_Product", () => shapeHelper.Parts_Product(
                Product: productModel));
        }
    }
}