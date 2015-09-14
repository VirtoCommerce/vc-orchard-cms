using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtoCommerce.WebStore.Models;
using VirtoCommerce.WebStore.Services;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Controllers
{
    public class ControllerBase : Controller
    {
        public ControllerBase(IOrchardServices orchardServices, IShoppingCartService cartService)
        {
            T = NullLocalizer.Instance;
            Settings = orchardServices.WorkContext.CurrentSite.As<WebStoreSettingsPart>();
            CustomerId = GetCustomerId(orchardServices.WorkContext);

            var cartViewModel = cartService.GetCartAsync(Settings.StoreId, CustomerId).Result;

            ShoppingCart = cartViewModel;
        }

        public Localizer T { get; set; }

        public WebStoreSettingsPart Settings { get; private set; }

        public string CustomerId { get; private set; }

        public Cart ShoppingCart { get; set; }

        public string GetModelErrorMessage(ModelStateDictionary modelState)
        {
            string errorMessage = T("Some unknown error").Text;

            var errorValues = modelState.Values.Where(v => v.Errors.Count > 0);
            if (errorValues != null)
            {
                var errorValue = errorValues.FirstOrDefault();
                if (errorValue != null)
                {
                    var error = errorValue.Errors.FirstOrDefault();
                    if (error != null)
                    {
                        errorMessage = error.ErrorMessage;
                    }
                }
            }
 
            return errorMessage;
        }

        private string GetCustomerId(WorkContext context)
        {
            var cookie = context.HttpContext.Request.Cookies[Settings.AnonymousCookieId];
            if (cookie == null)
            {
                cookie = new HttpCookie(Settings.AnonymousCookieId);
                cookie.Value = Guid.NewGuid().ToString();
                cookie.Expires = DateTime.UtcNow.AddDays(1);
                context.HttpContext.Response.SetCookie(cookie);
            }

            return cookie.Value;
        }
    }
}