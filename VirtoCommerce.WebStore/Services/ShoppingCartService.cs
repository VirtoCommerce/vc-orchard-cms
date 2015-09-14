using Orchard;
using Orchard.ContentManagement;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ApiClient;
using VirtoCommerce.WebStore.Converters;
using VirtoCommerce.WebStore.Models;
using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly WebStoreSettingsPart _settings;
        private readonly IVirtoCommerceClient _apiClient;
        private readonly IStoreService _storeService;

        public ShoppingCartService(IOrchardServices orchardServices, IVirtoCommerceClient apiClient, IStoreService storeService)
        {
            _settings = orchardServices.WorkContext.CurrentSite.As<WebStoreSettingsPart>();
            _apiClient = apiClient;
            _storeService = storeService;
        }

        public async Task CreateCartAsync(string storeId, string customerId)
        {
            var storeViewModel = await _storeService.GetStoreAsync(storeId).ConfigureAwait(false);

            var cart = new DataContracts.Cart.ShoppingCart();

            cart.LanguageCode = _settings.Culture;
            cart.Currency = storeViewModel.Currency;
            cart.Name = "default";
            cart.CustomerId = customerId;
            cart.StoreId = storeId;

            await _apiClient.CartClient.CreateCartAsync(cart);
        }

        public async Task<Cart> GetCartAsync(string storeId, string customerId)
        {
            var apiResponse = await _apiClient.CartClient.GetCartAsync(storeId, customerId).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var cartViewModel = apiResponse.ToViewModel();

            return cartViewModel;
        }

        public async Task<Checkout> GetCheckoutAsync(string storeId, string customerId)
        {
            var apiResponse = await _apiClient.CartClient.GetCartAsync(storeId, customerId).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var paymentMethods = await _apiClient.CartClient.GetCartPaymentMethods(apiResponse.Id).ConfigureAwait(false);
            var shippingMethods = await _apiClient.CartClient.GetCartShippingMethods(apiResponse.Id).ConfigureAwait(false);

            var checkoutViewModel = apiResponse.ToViewModel(paymentMethods, shippingMethods);

            return checkoutViewModel;
        }

        public async Task UpdateCartAsync(Cart cartViewModel)
        {
            var cart = cartViewModel.ToApiModel();

            await _apiClient.CartClient.UpdateCurrentCartAsync(cart).ConfigureAwait(false);
        }

        public async Task UpdateCartAsync(Checkout checkout)
        {
            var cart = checkout.ToApiModel();

            await _apiClient.CartClient.UpdateCurrentCartAsync(cart).ConfigureAwait(false);
        }

        public async Task DeleteCartAsync(string cartId)
        {
            await _apiClient.CartClient.DeleteCartAsync(new[] { cartId });
        }
    }
}