using Orchard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.Client.DataContracts;

namespace VirtoCommerce.Webshop.Client
{
    public interface IVirtoCommerceClient : IDependency
    {
        Task<string> TestApiConnectionAsync(string apiUrl, string appId, string secretKey);

        Task<ApiResponse<IEnumerable<Store>>> GetStoresAsync();

        Task<ApiResponse<ResponseCollection<Category>>> GetCategoriesAsync(ApiGetRequest request);

        Task<ApiResponse<Category>> GetCategoryAsync(ApiGetRequest request);

        Task<ApiResponse<ProductSearchResult>> SearchProductsAsync(ApiGetRequest request);

        Task<ApiResponse<Product>> GetProductAsync(ApiGetRequest request);

        Task<ApiResponse<IEnumerable<string>>> GetPricelistsAsync(string catalogId, string currency);

        Task<ApiResponse<IEnumerable<Price>>> GetPricesAsync(IEnumerable<string> pricelists, IEnumerable<string> productIds);

        void Dispose();
    }
}