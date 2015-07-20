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
    public class ProductListDriver : ContentPartDriver<ProductListPart>
    {
        private readonly IOrchardServices _orchardService;

        public ProductListDriver(IOrchardServices orchardServices)
        {
            T = NullLocalizer.Instance;

            _orchardService = orchardServices;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ProductListPart part, string displayType, dynamic shapeHelper)
        {
            var settings = _orchardService.WorkContext.CurrentSite.As<WebshopSettingsPart>();
            var apiClient = new ApiClient(settings.ApiUrl, settings.AppId, settings.SecretKey);

            string categoryId = null;
            var categorySegment = HttpContext.Current.Request.Url.Segments.FirstOrDefault(s => s.Equals("Category", StringComparison.OrdinalIgnoreCase));
            if (categorySegment != null)
            {
                categoryId = HttpContext.Current.Request.QueryString["id"];
            }

            var category = Task.Run(() => apiClient.GetCategoryAsync(categoryId)).Result;

            int page = 1;
            if (HttpContext.Current.Request.QueryString["p"] != null)
            {
                int.TryParse(HttpContext.Current.Request.QueryString["p"], out page);
            }

            int skip = (page - 1) * part.PageSize;
            int take = part.PageSize;

            var productsResponse = Task.Run(() => apiClient.GetProductsAsync(skip, take, category.Id)).Result;

            var productsIds = productsResponse.Items.Select(i => i.Id).ToArray();

            var pricesResponse = Task.Run(() => apiClient.GetPricesAsync(productsIds)).Result;

            var products = new List<Product>();
            foreach (var product in productsResponse.Items)
            {
                var price = pricesResponse.FirstOrDefault(p => p.ProductId == product.Id);
                products.Add(product.ToViewModel(price));
            }

            return ContentShape("Parts_ProductList", () => shapeHelper.Parts_ProductList(
                Title: category.Name,
                Slug: categoryId,
                Products: products,
                PagesCount: Math.Ceiling(productsResponse.Total / (double)part.PageSize),
                Page: page));
        }

        // GET
        protected override DriverResult Editor(ProductListPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_ProductList_Edit", () => shapeHelper.EditorTemplate(
                TemplateName: "Parts/ProductList",
                Model: part,
                Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(ProductListPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}