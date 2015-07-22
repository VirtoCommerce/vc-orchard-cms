using Orchard;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using VirtoCommerce.Webshop.Client.DataContracts;
using VirtoCommerce.Webshop.Client.Security;
using VirtoCommerce.Webshop.Models;

namespace VirtoCommerce.Webshop.Client
{
    public class VirtoCommerceClient : IVirtoCommerceClient
    {
        private readonly IOrchardServices _orchardServices;
        private readonly Lazy<WebshopSettingsPart> _webshopSettings;
        private readonly Lazy<HttpClient> _httpClient;
        private bool _disposed;

        public VirtoCommerceClient(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
            _webshopSettings = new Lazy<WebshopSettingsPart>(GetWebshopSettings);
            _httpClient = new Lazy<HttpClient>(GetHttpClient);
        }

        public string ApiUrl
        {
            get
            {
                return _webshopSettings.Value.ApiUrl;
            }
        }

        public string AppId
        {
            get
            {
                return _webshopSettings.Value.AppId;
            }
        }

        public string SecretKey
        {
            get
            {
                return _webshopSettings.Value.SecretKey;
            }
        }

        public async Task<string> TestApiConnectionAsync(string apiUrl, string appId, string secretKey)
        {
            string requestUrl = String.Format("{0}/mp/stores", ApiUrl);
            using (var httpResponse = await _httpClient.Value.GetAsync(requestUrl).ConfigureAwait(false))
            {
                return String.Format("{0} {1}", (int)httpResponse.StatusCode, httpResponse.ReasonPhrase);
            }
        }

        public async Task<ApiResponse<IEnumerable<Store>>> GetStoresAsync()
        {
            string requestUrl = String.Format("{0}/mp/stores", ApiUrl);
            using (var httpResponse = await _httpClient.Value.GetAsync(requestUrl).ConfigureAwait(false))
            {
                return await GetApiResponseAsync<IEnumerable<Store>>(httpResponse);
            }
        }

        public async Task<ApiResponse<ResponseCollection<Category>>> GetCategoriesAsync(ApiGetRequest request)
        {
            string requestUrl = String.Format("{0}/mp/categories?{1}&parentId={2}", ApiUrl, request.BuildQueryString(), null);
            using (var httpResponse = await _httpClient.Value.GetAsync(requestUrl).ConfigureAwait(false))
            {
                return await GetApiResponseAsync<ResponseCollection<Category>>(httpResponse);
            }
        }

        public async Task<ApiResponse<Category>> GetCategoryAsync(ApiGetRequest request)
        {
            string requestUrl = String.Format("{0}/mp/categories?{1}", ApiUrl, request.BuildQueryString());
            using (var httpResponse = await _httpClient.Value.GetAsync(requestUrl).ConfigureAwait(false))
            {
                return await GetApiResponseAsync<Category>(httpResponse);
            }
        }

        public async Task<ApiResponse<ProductSearchResult>> SearchProductsAsync(ApiGetRequest request)
        {
            string requestUrl = String.Format("{0}/mp/products/search?{1}", ApiUrl, request.BuildQueryString());
            using (var httpResponse = await _httpClient.Value.GetAsync(requestUrl).ConfigureAwait(false))
            {
                return await GetApiResponseAsync<ProductSearchResult>(httpResponse);
            }
        }

        public async Task<ApiResponse<Product>> GetProductAsync(ApiGetRequest request)
        {
            string requestUrl = String.Format("{0}/mp/products?{1}", ApiUrl, request.BuildQueryString());
            using (var httpResponse = await _httpClient.Value.GetAsync(requestUrl).ConfigureAwait(false))
            {
                return await GetApiResponseAsync<Product>(httpResponse);
            }
        }

        public async Task<ApiResponse<IEnumerable<string>>> GetPricelistsAsync(string catalogId, string currency)
        {
            string requestUrl = String.Format("{0}/mp/pricelists?catalog={1}&currency={2}", ApiUrl, catalogId, currency);
            using (var httpResponse = await _httpClient.Value.GetAsync(requestUrl).ConfigureAwait(false))
            {
                return await GetApiResponseAsync<IEnumerable<string>>(httpResponse);
            }
        }

        public async Task<ApiResponse<IEnumerable<Price>>> GetPricesAsync(IEnumerable<string> pricelists, IEnumerable<string> productIds)
        {
            string requestUrl = String.Format("{0}/mp/prices?pricelists={1}&products={2}", ApiUrl, String.Join(",", pricelists), String.Join(",", productIds));
            using (var httpResponse = await _httpClient.Value.GetAsync(requestUrl).ConfigureAwait(false))
            {
                return await GetApiResponseAsync<IEnumerable<Price>>(httpResponse);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_httpClient.IsValueCreated)
                {
                    _httpClient.Value.Dispose();
                }
            }

            _disposed = true;
        }

        private WebshopSettingsPart GetWebshopSettings()
        {
            return _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient(new HmacMessageProcessingHandler(_webshopSettings.Value.AppId, _webshopSettings.Value.SecretKey));
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            return httpClient;
        }

        private async Task<ApiResponse<T>> GetApiResponseAsync<T>(HttpResponseMessage httpResponse) where T : class
        {
            var apiResponse = new ApiResponse<T>();

            if (httpResponse.IsSuccessStatusCode)
            {
                apiResponse.Body = await httpResponse.Content.ReadAsAsync<T>();
            }
            else
            {
                apiResponse.Error = await HandleErrorAsync(httpResponse);
            }

            return apiResponse;
        }

        private async Task<ManagementError> HandleErrorAsync(HttpResponseMessage httpResponse)
        {
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return await httpResponse.Content.ReadAsAsync<ManagementError>();
            }
            else
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }
        }
    }
}