using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VirtoCommerce.Webshop.Client;
using VirtoCommerce.Webshop.Converters;
using VirtoCommerce.Webshop.Models;

namespace VirtoCommerce.Webshop.Drivers
{
    public class ProductDriver : ContentPartDriver<ProductPart>
    {
        private readonly IOrchardServices _orchardServices;

        public ProductDriver(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ProductPart part, string displayType, dynamic shapeHelper)
        {
            var settings = _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();
            var apiClient = new ApiClient(settings.ApiUrl, settings.AppId, settings.SecretKey);

            string productId = HttpContext.Current.Request.QueryString["id"];

            var product = Task.Run(() => apiClient.GetProductAsync(productId)).Result;

            var pricesResponse = Task.Run(() => apiClient.GetPricesAsync(new[] { product.Id })).Result;
            var price = pricesResponse.FirstOrDefault(p => p.ProductId == product.Id);

            var productModel = product.ToViewModel(price);

            return ContentShape("Parts_Product", () => shapeHelper.Parts_Product(
                Product: productModel));
        }
    }
}