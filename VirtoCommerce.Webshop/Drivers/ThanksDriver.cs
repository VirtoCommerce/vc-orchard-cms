using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System.Web;
using VirtoCommerce.Webshop.Models;
using VirtoCommerce.Webshop.Services;

namespace VirtoCommerce.Webshop.Drivers
{
    public class ThanksDriver : ContentPartDriver<ThanksPart>
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IOrderService _orderService;

        public ThanksDriver(IOrchardServices orchardServices, IOrderService orderService)
        {
            _orchardServices = orchardServices;
            _orderService = orderService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Display(ThanksPart part, string displayType, dynamic shapeHelper)
        {
            var settings = _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();

            string customerId = null;
            var anonymousCookie = HttpContext.Current.Request.Cookies[settings.AnonymousCookieId];
            if (anonymousCookie != null)
            {
                customerId = anonymousCookie.Value;
            }

            var orderId = HttpContext.Current.Request.QueryString["id"];

            var order = _orderService.GetOrderAsync(customerId, orderId).Result;

            return ContentShape("Parts_Thanks", () => shapeHelper.Parts_Thanks(
                Order: order));
        }
    }
}