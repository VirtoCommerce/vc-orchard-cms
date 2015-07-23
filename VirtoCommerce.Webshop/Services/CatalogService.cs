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
                if (apiResponse.Content != null)
                {
                    foreach (var category in apiResponse.Content.Items)
                    {
                        categoryModels.Add(category.ToViewModel());
                    }
                }
            }

            return categoryModels;
        }

        public async Task<Category> GetCategoryAsync(string storeId, string culture, string slug)
        {
            Category categoryModel = null;

            var apiRequest = new ApiGetRequest
            {
                Culture = culture,
                Keyword = slug,
                StoreId = storeId
            };

            var apiResponse = await _apiClient.GetCategoryAsync(apiRequest).ConfigureAwait(false);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Content != null)
                {
                    categoryModel = apiResponse.Content.ToViewModel();
                }
            }

            return categoryModel;
        }

        public async Task<PagedList<Product>> SearchProductsAsync(string storeId, string culture, string categorySlug, int skip, int take, IEnumerable<string> pricelists)
        {
            PagedList<Product> productPagedList = null;

            var apirequest = new ApiGetRequest
            {
                Culture = culture,
                Outline = categorySlug,
                PricelistIds = pricelists,
                Skip = skip,
                Take = take,
                ResponseGroup = 102,
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
                if (apiResponse.Content != null)
                {
                    var productIds = apiResponse.Content.Items.Select(i => i.Id).ToList();
                    foreach (var product in apiResponse.Content.Items)
                    {
                        if (product.Variations != null)
                        {
                            productIds.AddRange(product.Variations.Select(v => v.Id));
                        }
                    }

                    var priceModels = await _priceService.GetPricesAsync(pricelists, productIds);

                    var productModels = new List<Product>();
                    foreach (var product in apiResponse.Content.Items)
                    {
                        var priceModel = priceModels.FirstOrDefault(p => p.ProductId.Equals(product.Id));

                        productModels.Add(product.ToViewModel(priceModel));
                    }

                    productPagedList = new PagedList<Product>(productModels, take, apiResponse.Content.Total);
                }
            }

            return productPagedList;
        }

        public async Task<Product> GetProductBySlugAsync(string storeId, string culture, string pricelistId, string slug)
        {
            Product productModel = null;

            var apiRequest = new ApiGetRequest
            {
                Culture = culture,
                Keyword = slug,
                StoreId = storeId
            };

            var apiResponse = await _apiClient.GetProductAsync(apiRequest).ConfigureAwait(false);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Content != null)
                {
                    var productIds = new List<string>();
                    productIds.Add(apiResponse.Content.Id);

                    if (apiResponse.Content.Variations != null)
                    {
                        productIds.AddRange(apiResponse.Content.Variations.Select(v => v.Id));
                    }

                    var priceModels = await _priceService.GetPricesAsync(new[] { pricelistId }, productIds).ConfigureAwait(false);
                    var productPriceModel = priceModels.FirstOrDefault(p => p.ProductId == apiResponse.Content.Id);

                    productModel = apiResponse.Content.ToViewModel(productPriceModel);
                }
            }

            return productModel;
        }

        public async Task<Product> GetProductBySkuAsync(string storeId, string culture, string pricelistId, string sku)
        {
            Product productModel = null;

            var apiRequest = new ApiGetRequest
            {
                Culture = culture,
                Code = sku,
                StoreId = storeId
            };

            var apiResponse = await _apiClient.GetProductAsync(apiRequest).ConfigureAwait(false);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Content != null)
                {
                    var productIds = new List<string>();
                    productIds.Add(apiResponse.Content.Id);

                    if (apiResponse.Content.Variations != null)
                    {
                        productIds.AddRange(apiResponse.Content.Variations.Select(v => v.Id));
                    }

                    var priceModels = await _priceService.GetPricesAsync(new[] { pricelistId }, productIds).ConfigureAwait(false);
                    var productPriceModel = priceModels.FirstOrDefault(p => p.ProductId == apiResponse.Content.Id);

                    productModel = apiResponse.Content.ToViewModel(productPriceModel);
                }
            }

            return productModel;
        }
    }
}