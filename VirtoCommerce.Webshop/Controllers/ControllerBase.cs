using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using System;
using System.Web;
using System.Web.Mvc;
using VirtoCommerce.Webshop.Models;

namespace VirtoCommerce.Webshop.Controllers
{
    public class ControllerBase : Controller
    {
        public ControllerBase(IOrchardServices orchardServices)
        {
            Settings = orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public WebshopSettingsPart Settings { get; private set; }

        public string GetCustomerId(HttpContextBase httpContext, string anonymousCookieId)
        {
            string customerId = null;

            var anonymousCookie = httpContext.Request.Cookies[anonymousCookieId];
            if (anonymousCookie == null)
            {
                customerId = Guid.NewGuid().ToString();
                anonymousCookie = new HttpCookie(anonymousCookieId, customerId);
                httpContext.Response.AppendCookie(anonymousCookie);
            }
            else
            {
                customerId = anonymousCookie.Value;
            }

            return customerId;
        }
    }
}