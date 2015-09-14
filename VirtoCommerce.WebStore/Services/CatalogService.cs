using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ApiClient;
using VirtoCommerce.WebStore.Converters;
using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IVirtoCommerceClient _apiClient;
        private readonly IStoreService _storeService;
        private readonly IPriceService _priceService;

        public CatalogService(IVirtoCommerceClient apiClient, IStoreService storeService, IPriceService priceService)
        {
            _apiClient = apiClient;
            _storeService = storeService;
            _priceService = priceService;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string storeId, string culture)
        {
            var apiResponse = await _apiClient.BrowseClient.GetCategoriesAsync(storeId, culture).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var categoryViewModels = apiResponse.Items.Select(c => c.ToViewModel());

            return categoryViewModels;
        }

        public async Task<PagedList<Product>> GetProductsAsync(string storeId, string culture, string categorySlug, int skip, int take)
        {
            string outline = null;
            if (!string.IsNullOrEmpty(categorySlug))
            {
                var category = await _apiClient.BrowseClient.GetCategoryByKeywordAsync(storeId, culture, categorySlug).ConfigureAwait(false);
                var categoryViewModel = category.ToViewModel();

                outline = BuildOutline(categoryViewModel);
            }

            var browseQuery = new DataContracts.BrowseQuery
            {
                Outline = outline,
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

            var storeViewModel = await _storeService.GetStoreAsync(storeId).ConfigureAwait(false);

            var priceViewModels = await _priceService.GetPricesAsync(storeViewModel.CatalogId, storeViewModel.Currency, productIds).ConfigureAwait(false);

            var productViewModels = new List<Product>();

            foreach (var product in apiResponse.Items)
            {
                var priceViewModel = priceViewModels.FirstOrDefault(p => p.ProductId == product.Id);

                productViewModels.Add(product.ToViewModel(priceViewModel));
            }

            return new PagedList<Product>(productViewModels, pageIndex, take, string.IsNullOrEmpty(categorySlug) ? apiResponse.Items.Count : apiResponse.TotalCount);
        }

        public async Task<Product> GetProductAsync(string storeId, string culture, string slug)
        {
            var apiResponse = await _apiClient.BrowseClient.GetProductByKeywordAsync(storeId, culture, slug).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var storeViewModel = await _storeService.GetStoreAsync(storeId).ConfigureAwait(false);

            var priceViewModels = await _priceService.GetPricesAsync(storeViewModel.CatalogId, storeViewModel.Currency, new[] { apiResponse.Id }).ConfigureAwait(false);

            var productViewModel = apiResponse.ToViewModel(priceViewModels.FirstOrDefault());

            return productViewModel;
        }

        private string BuildOutline(Category category)
        {
            var segments = category.ParentCategories.Select(c => c.Id).ToList();
            segments.Add(category.Id);

            string outline = string.Join("/", segments);

            return outline;
        }
    }
}