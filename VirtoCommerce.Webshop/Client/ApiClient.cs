using System;
using System.Net.Http;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.Client.DataContracts;

namespace VirtoCommerce.Webshop.Client
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        private bool _disposed;

        private const string StoreId = "SampleStore";
        private const string Locale = "en-US";

        public ApiClient(string apiUrl, string appId, string secretKey)
        {
            ApiUrl = apiUrl;
            AppId = appId;
            SecretKey = secretKey;

            var hmacMessageProcessingHandler = new HmacMessageProcessingHandler(appId, secretKey);

            _httpClient = new HttpClient(hmacMessageProcessingHandler);
            _httpClient.Timeout = TimeSpan.FromSeconds(90);
        }

        public string ApiUrl { get; set; }

        public string AppId { get; set; }

        public string SecretKey { get; set; }

        public async Task<string> TestUrlConnectionAsync()
        {
            string requestString = String.Format("{0}/mp/stores", ApiUrl);
            using (var response = await _httpClient.GetAsync(requestString).ConfigureAwait(false))
            {
                return String.Format("{0} {1}", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public async Task<ResponseCollection<Category>> GetCategoriesAsync()
        {
            string requestString = String.Format("{0}/mp/categories?store={1}&language={2}&parentId={3}", ApiUrl, StoreId, Locale, null);
            using (var response = await _httpClient.GetAsync(requestString).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ResponseCollection<Category>>().ConfigureAwait(false);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<Category> GetCategoryAsync(string keyword)
        {
            string requestString = String.Format("{0}/mp/categories?store={1}&language={2}&keyword={3}", ApiUrl, StoreId, Locale, keyword);
            using (var response = await _httpClient.GetAsync(requestString).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Category>().ConfigureAwait(false);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<ProductSearchResult> GetProductsAsync(int skip, int take, string categoryId)
        {
            string requestString = String.Format(
                "{0}/mp/products/search?store={1}&language={2}&outline={3}&pricelists={4}&responseGroup={5}&skip={6}&take={7}",
                ApiUrl, StoreId, Locale, categoryId, "SaleUSD,DefaultUSD", 102, skip, take);
            using (var response = await _httpClient.GetAsync(requestString).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ProductSearchResult>().ConfigureAwait(false);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<Price[]> GetPricesAsync(string[] productIds)
        {
            string requestString = String.Format("{0}/mp/prices?pricelists=SaleUSD,DefaultUSD&products={1}", ApiUrl, String.Join(",", productIds));
            using (var response = await _httpClient.GetAsync(requestString).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Price[]>().ConfigureAwait(false);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<Product> GetProductAsync(string code)
        {
            string requestString = String.Format("{0}/mp/products?store={1}&language={2}&keyword={3}", ApiUrl, StoreId, Locale, code);
            using (var response = await _httpClient.GetAsync(requestString).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Product>().ConfigureAwait(false);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if(_httpClient != null)
                {
                    _httpClient.Dispose();
                }
            }

            _disposed = true;
        }
    }
}