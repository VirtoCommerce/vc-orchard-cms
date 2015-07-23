using Orchard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.Client.DataContracts;
using VirtoCommerce.Webshop.Client.DataContracts.Cart;
using VirtoCommerce.Webshop.Client.DataContracts.Order;

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

        Task<ApiResponse<IEnumerable<string>>> GetPricelistsAsync(ApiGetRequest request);

        Task<ApiResponse<IEnumerable<Price>>> GetPricesAsync(ApiGetRequest request);

        Task<ApiResponse<object>> CreateShoppingCartAsync(ShoppingCart shoppingCart);

        Task<ApiResponse<ShoppingCart>> GetShoppingCartAsync(ApiGetRequest request);

        Task<ApiResponse<object>> UpdateShoppingCartAsync(ShoppingCart shoppingCart);

        Task<ApiResponse<Object>> DeleteShoppingCartAsync(IEnumerable<string> shoppingCartIds);

        void Dispose();
    }
}