using System;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.Client;
using VirtoCommerce.Webshop.Converters;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IVirtoCommerceClient _apiClient;

        public ShoppingCartService(IVirtoCommerceClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string storeId, string customerId)
        {
            ShoppingCart shoppingCartModel = null;

            var apiRequest = new ApiGetRequest
            {
                CustomerId = customerId,
                StoreId = storeId
            };

            var apiResponse = await _apiClient.GetShoppingCartAsync(apiRequest).ConfigureAwait(false);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Content != null)
                {
                    return apiResponse.Content.ToViewModel();
                }
            }

            return shoppingCartModel;
        }

        public async Task CreateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            var apiResponse = await _apiClient.CreateShoppingCartAsync(shoppingCart.ToApiModel());
            if (apiResponse.Error != null)
            {
                // TODO: Do something with errors
                throw new Exception(apiResponse.Error.StackTrace);
            }
        }

        public async Task UpdateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            var apiResponse = await _apiClient.UpdateShoppingCartAsync(shoppingCart.ToApiModel());
            if (apiResponse.Error != null)
            {
                // TODO: Do something with errors
                throw new Exception(apiResponse.Error.StackTrace);
            }
        }

        public async Task DeleteShoppingCartAsync(string shoppingCartId)
        {
            var apiResponse = await _apiClient.DeleteShoppingCartAsync(new[] { shoppingCartId });
            if (apiResponse.Error != null)
            {
                // TODO: Do something with errors
                throw new Exception(apiResponse.Error.StackTrace);
            }
        }
    }
}