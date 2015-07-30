using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtoCommerce.Webshop.Models;
using VirtoCommerce.Webshop.Services;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Drivers
{
    public class ProductListDriver : ContentPartDriver<ProductListPart>
    {
        private readonly IOrchardServices _orchardServices;
        private readonly ICatalogService _catalogService;
        private readonly IPriceService _priceService;

        public ProductListDriver(IOrchardServices orchardServices, ICatalogService catalogService, IPriceService priceService)
        {
            _orchardServices = orchardServices;
            _catalogService = catalogService;
            _priceService = priceService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ProductListPart part, string displayType, dynamic shapeHelper)
        {
            var settings = _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();
            var httpRequest = HttpContext.Current.Request;

            string categorySlug = null;
            if (httpRequest.Url.Segments.Any(s => s.Equals("Category", StringComparison.OrdinalIgnoreCase)))
            {
                categorySlug = httpRequest.QueryString["id"];
            }

            string categoryId = null;
            if (!string.IsNullOrEmpty(categorySlug))
            {
                var categoryModel = _catalogService.GetCategoryAsync(settings.StoreId, settings.Culture, categorySlug).Result;
                categoryId = categoryModel.Id;
            }

            int page = 1;
            if (httpRequest.QueryString["p"] != null)
            {
                int.TryParse(httpRequest.QueryString["p"], out page);
            }

            var shopModel = _catalogService.GetShopsAsync().Result.FirstOrDefault(s => s.Id == settings.StoreId);
            var pricelistIds = _priceService.GetPricelistsAsync(shopModel.CatalogId, shopModel.Currency).Result;

            var productList = _catalogService.SearchProductsAsync(settings.StoreId, settings.Culture, categoryId, pricelistIds, (page - 1) * settings.PageSize, settings.PageSize).Result;

            return ContentShape("Parts_ProductList", () => shapeHelper.Parts_ProductList(
                CategorySlug: categorySlug,
                ProductList: productList));
        }
    }
}