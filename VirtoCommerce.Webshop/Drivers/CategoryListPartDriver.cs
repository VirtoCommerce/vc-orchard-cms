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
    public class CategoryListPartDriver : ContentPartDriver<CategoryListPart>
    {
        private const string StoreId = "SampleStore";
        private const string Culture = "en-US";

        private readonly ICatalogService _catalogService;

        public CategoryListPartDriver(ICatalogService catalogService)
        {
            _catalogService = catalogService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(CategoryListPart part, string displayType, dynamic shapeHelper)
        {
            var categoryModels = _catalogService.GetCategoriesAsync(StoreId, Culture).Result;

            string selectedCategorySlug = null;
            if (HttpContext.Current.Request.Url.Segments.Any(s => s.Equals("Category", StringComparison.OrdinalIgnoreCase)))
            {
                selectedCategorySlug = HttpContext.Current.Request.QueryString["id"];
            }

            return ContentShape("Parts_CategoryList", () => shapeHelper.Parts_CategoryList(
                SelectedCategorySlug: selectedCategorySlug,
                Categories: categoryModels));
        }
    }
}