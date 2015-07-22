using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string storeId, string culture)
        {
            var categoryModels = new List<Category>();

            var apiRequest = new ApiGetRequest
            {
                Culture = culture,
                StoreId = storeId
            };

            var apiResponse = await _apiClient.GetCategoriesAsync(apiRequest).ConfigureAwait(false);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Body != null)
                {
                    foreach (var category in apiResponse.Body.Items)
                    {
                        categoryModels.Add(category.ToViewModel());
                    }
                }
            }

            return categoryModels;
        }

        // Not implemented yet
        public Task<Category> GetCategoryAsync(string storeId, string culture, string slug)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PagedList<Product>> SearchProductsAsync(string storeId, string culture, string categorySlug, int skip, int take, IEnumerable<string> pricelists)
        {
            PagedList<Product> productPagedList = null;

            var apirequest = new ApiGetRequest
            {
                Culture = culture,
                Outline = categorySlug,
                Pricelists = pricelists,
                ResponseGroup = 100,
                Skip = skip,
                Take = take,
                StoreId = storeId
            };

            var apiResponse = await _apiClient.SearchProductsAsync(apirequest).ConfigureAwait(false);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Body != null)
                {
                    var productIds = apiResponse.Body.Items.Select(i => i.Id).ToList();
                    foreach (var product in apiResponse.Body.Items)
                    {
                        if (product.Variations != null)
                        {
                            productIds.AddRange(product.Variations.Select(v => v.Id));
                        }
                    }

                    var priceModels = await _priceService.GetPricesAsync(pricelists, productIds);

                    var productModels = new List<Product>();
                    foreach (var product in apiResponse.Body.Items)
                    {
                        var priceModel = priceModels.FirstOrDefault(p => p.ProductId.Equals(product.Id));

                        productModels.Add(product.ToViewModel(priceModel));
                    }

                    productPagedList = new PagedList<Product>(productModels, take, apiResponse.Body.Total);
                }
            }

            return productPagedList;
        }

        // Not implemented yet
        public Task<Product> GetProductAsync(string storeId, string culture, string slug)
        {
            throw new System.NotImplementedException();
        }
    }
}