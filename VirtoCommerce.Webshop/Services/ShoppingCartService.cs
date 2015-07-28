using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateShoppingCartAsync(ShoppingCart shoppingCart)
        {
            var shoppingCartModel = shoppingCart.ToApiModel();

            await _apiClient.CartClient.CreateCartAsync(shoppingCartModel).ConfigureAwait(false);
        }

        public async Task DeleteShoppingCartAsync(string shoppingCartId)
        {
            await _apiClient.CartClient.DeleteCartAsync(new[] { shoppingCartId }).ConfigureAwait(false);
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string storeId, string customerId)
        {
            var apiResponse = await _apiClient.CartClient.GetCartAsync(storeId, customerId).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.ToViewModel();
        }

        public async Task UpdateShoppingCartAsync(ShoppingCart shoppingCart, Checkout checkout)
        {
            var shoppingCartModel = shoppingCart.ToApiModel();

            if (checkout != null)
            {
                shoppingCartModel = shoppingCart.ToApiModel(checkout);
            }

            await _apiClient.CartClient.UpdateCurrentCartAsync(shoppingCartModel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ShippingMethod>> GetShippingMethodsAsync(string shoppingCartId)
        {
            var apiResponse = await _apiClient.CartClient.GetCartShippingMethods(shoppingCartId).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.Select(r => r.ToViewModel());
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(string shoppingCartId)
        {
            var apiResponse = await _apiClient.CartClient.GetCartPaymentMethods(shoppingCartId).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.Select(r => r.ToViewModel());
        }

        public async Task<Checkout> GetCheckoutAsync(string storeId, string customerId)
        {
            Checkout checkoutModel = null;

            var shoppingCart = await _apiClient.CartClient.GetCartAsync(storeId, customerId).ConfigureAwait(false);
            if (shoppingCart != null)
            {
                var paymentMethodModels = await GetPaymentMethodsAsync(shoppingCart.Id).ConfigureAwait(false);
                var shippingMethodModels = await GetShippingMethodsAsync(shoppingCart.Id).ConfigureAwait(false);

                checkoutModel = shoppingCart.ToViewModel(paymentMethodModels, shippingMethodModels);
            }

            return checkoutModel;
        }
    }
}