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
    public class CategoryListDriver : ContentPartDriver<CategoryListPart>
    {
        private readonly IOrchardServices _orchardServices;
        private readonly ICatalogService _catalogService;

        public CategoryListDriver(IOrchardServices orchardServices, ICatalogService catalogService)
        {
            _orchardServices = orchardServices;
            _catalogService = catalogService;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(CategoryListPart part, string displayType, dynamic shapeHelper)
        {
            var settings = _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();
            var httpRequest = HttpContext.Current.Request;

            var categories = _catalogService.GetCategoriesAsync(settings.StoreId, settings.Culture).Result;

            string categorySlug = null;
            if (httpRequest.Url.Segments.Any(s => s.Equals("Category", StringComparison.OrdinalIgnoreCase)))
            {
                categorySlug = httpRequest.QueryString["id"];
            }

            return ContentShape("Parts_CategoryList", () => shapeHelper.Parts_CategoryList(
                CategorySlug: categorySlug,
                Categories: categories));
        }
    }
}