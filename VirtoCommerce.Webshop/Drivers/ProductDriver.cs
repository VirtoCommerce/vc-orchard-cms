using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System;
using System.Linq;
using System.Web;
using VirtoCommerce.Webshop.Models;
using VirtoCommerce.Webshop.Services;

namespace VirtoCommerce.Webshop.Drivers
{
    public class ProductDriver : ContentPartDriver<ProductPart>
    {
        private readonly IOrchardServices _orchardServices;
        private readonly ICatalogService _catalogService;

        public ProductDriver(IOrchardServices orchardServices, ICatalogService catalogService)
        {
            _orchardServices = orchardServices;
            _catalogService = catalogService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ProductPart part, string displayType, dynamic shapeHelper)
        {
            var settings = _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();
            var httpRequest = HttpContext.Current.Request;

            string productSlug = null;
            if(httpRequest.Url.Segments.Any(s => s.Equals("Product", StringComparison.OrdinalIgnoreCase)))
            {
                productSlug = httpRequest.QueryString["id"];
            }

            var productModel = _catalogService.GetProductAsync(settings.StoreId, settings.Culture, productSlug).Result;

            return ContentShape("Parts_Product", () => shapeHelper.Parts_Product(
                Product: productModel));
        }
    }
}