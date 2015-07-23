using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VirtoCommerce.Webshop.Models;
using VirtoCommerce.Webshop.Services;

namespace VirtoCommerce.Webshop.Drivers
{
    public class CategoryListDriver : ContentPartDriver<CategoryListPart>
    {
        private const string StoreId = "SampleStore";
        private const string Culture = "en-US";

        private readonly ICatalogService _catalogService;

        public CategoryListDriver(ICatalogService catalogService)
        {
            _catalogService = catalogService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(CategoryListPart part, string displayType, dynamic shapeHelper)
        {
            var categoryModels = _catalogService.GetCategoriesAsync(StoreId, Culture).Result;

            string categorySlug = null;
            if (HttpContext.Current.Request.Url.Segments.Any(s => s.Equals("Category", StringComparison.OrdinalIgnoreCase)))
            {
                categorySlug = HttpContext.Current.Request.QueryString["slug"];
            }

            return ContentShape("Parts_CategoryList", () => shapeHelper.Parts_CategoryList(
                CategorySlug: categorySlug,
                Categories: categoryModels));
        }
    }
}