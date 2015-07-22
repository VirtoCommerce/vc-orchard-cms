using Orchard.UI.Admin;
using System.Threading.Tasks;
using System.Web.Mvc;
using VirtoCommerce.Webshop.Client;

namespace VirtoCommerce.Webshop.Controllers
{
    [Admin]
    public class WebshopSettingsController : Controller
    {
        private readonly IVirtoCommerceClient _apiClient;

        public WebshopSettingsController(IVirtoCommerceClient apiClient)
        {
            _apiClient = apiClient;
        }

        // POST: WebshopSettings/TestApiConnection
        [HttpPost]
        public async Task<JsonResult> TestApiConnection(string apiUrl, string appId, string secretKey)
        {
            var message = await _apiClient.TestApiConnectionAsync(apiUrl, appId, secretKey);

            return Json(message);
        }
    }
}