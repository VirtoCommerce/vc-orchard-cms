using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.Client;
using VirtoCommerce.Webshop.Converters;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IVirtoCommerceClient _apiClient;
        private readonly IPriceService _priceService;

        public CatalogService(IVirtoCommerceClient apiClient, IPriceService priceService)
        {
            _apiClient = apiClient;
            _priceService = priceService;
        }

        public async Task<IEnumerable<Shop>> GetShopsAsync()
        {
            var apiResponse = await _apiClient.StoreClient.GetStoresAsync().ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.Select(s => s.ToViewModel());
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string storeId, string culture)
        {
            var apiResponse = await _apiClient.BrowseClient.GetCategoriesAsync(storeId, culture).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var categoryModels = new List<Category>();

            foreach (var category in apiResponse.Items)
            {
                categoryModels.Add(category.ToViewModel());
            }

            return categoryModels;
        }

        public async Task<Category> GetCategoryAsync(string storeId, string culture, string slug)
        {
            var apiResponse = await _apiClient.BrowseClient.GetCategoryByKeywordAsync(storeId, culture, slug).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.ToViewModel();
        }

        public async Task<PagedList<Product>> SearchProductsAsync(string storeId, string culture, string categorySlug, IEnumerable<string> pricelistIds, int skip, int take)
        {
            var browseQuery = new DataContracts.BrowseQuery
            {
                Outline = categorySlug,
                PriceLists = pricelistIds.ToArray(),
                Skip = skip,
                Take = take
            };

            var apiResponse = await _apiClient.BrowseClient.GetProductsAsync(storeId, culture, browseQuery, DataContracts.ItemResponseGroups.ItemMedium).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            int pageIndex = skip / take + 1;

            var productIds = apiResponse.Items.Select(i => i.Id);

            var priceModels = await _priceService.GetPricesAsync(pricelistIds, productIds).ConfigureAwait(false);

            var productModels = apiResponse.Items.Select(i => i.ToViewModel(priceModels.FirstOrDefault(p => p.ProductId == i.Id)));

            return new PagedList<Product>(productModels, pageIndex, take, apiResponse.TotalCount);
        }

        public async Task<Product> GetProductAsync(string storeId, string culture, string slug)
        {
            var apiResponse = await _apiClient.BrowseClient.GetProductByKeywordAsync(storeId, culture, slug).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var stores = await _apiClient.StoreClient.GetStoresAsync().ConfigureAwait(false);
            var store = stores.FirstOrDefault(s => s.Id == storeId);

            var pricelistIds = await _priceService.GetPricelistsAsync(store.Catalog, store.DefaultCurrency).ConfigureAwait(false);

            var priceModels = await _priceService.GetPricesAsync(pricelistIds, new[] { apiResponse.Id }).ConfigureAwait(false);

            return apiResponse.ToViewModel(priceModels.FirstOrDefault(p => p.ProductId == apiResponse.Id));
        }
    }
}