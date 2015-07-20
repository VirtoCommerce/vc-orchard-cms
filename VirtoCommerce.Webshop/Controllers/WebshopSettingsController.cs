using Orchard.Localization;
using Orchard.UI.Admin;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using VirtoCommerce.Webshop.Client;

namespace VirtoCommerce.Webshop.Controllers
{
    [Admin]
    public class WebshopSettingsController : Controller
    {
        public Localizer T { get; set; }

        public WebshopSettingsController()
        {
            T = NullLocalizer.Instance;
        }

        // POST: WebshopSettings/TestApiConnection
        [HttpPost]
        public async Task<JsonResult> TestApiConnection(string apiUrl, string appId, string secretKey)
        {
            string message = String.Empty;

            if (String.IsNullOrEmpty(apiUrl))
            {
                message = T("API URL was not setted.").Text;
            }
            if (String.IsNullOrEmpty(appId))
            {
                message = T("Application ID was not setted.").Text;
            }
            if (String.IsNullOrEmpty(secretKey))
            {
                message = T("Secret key was not setted.").Text;
            }

            if (!String.IsNullOrEmpty(apiUrl) && !String.IsNullOrEmpty(appId) && !String.IsNullOrEmpty(secretKey))
            {
                var apiClient = new ApiClient(apiUrl, appId, secretKey);

                message = await apiClient.TestUrlConnectionAsync();

                apiClient.Dispose();
            }

            return Json(new { message = message });
        }
    }
}