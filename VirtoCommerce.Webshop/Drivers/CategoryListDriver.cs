using JetBrains.Annotations;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VirtoCommerce.Webshop.Client;
using VirtoCommerce.Webshop.Converters;
using VirtoCommerce.Webshop.Models;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Drivers
{
    [UsedImplicitly]
    public class CategoryListDriver : ContentPartDriver<CategoryListPart>
    {
        private readonly IOrchardServices _orchardServices;

        public CategoryListDriver(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(CategoryListPart part, string displayType, dynamic shapeHelper)
        {
            var settings = _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();
            var apiClient = new ApiClient(settings.ApiUrl, settings.AppId, settings.SecretKey);

            string categoryId = null;
            var categorySegment = HttpContext.Current.Request.Url.Segments.FirstOrDefault(s => s.Equals("Category", StringComparison.OrdinalIgnoreCase));
            if (categorySegment != null)
            {
                categoryId = HttpContext.Current.Request.QueryString["id"];
            }

            var categoriesResponse = Task.Run(() => apiClient.GetCategoriesAsync()).Result;

            var categories = new List<Category>();
            if (categoriesResponse != null)
            {
                foreach (var category in categoriesResponse.Items)
                {
                    categories.Add(category.ToViewModel());
                }
            }

            return ContentShape("Parts_CategoryList", () => shapeHelper.Parts_CategoryList(
                Categories: categories,
                SelectedCategory: categoryId));
        }
    }
}