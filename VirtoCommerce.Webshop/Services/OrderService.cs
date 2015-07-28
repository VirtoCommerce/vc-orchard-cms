using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.Client;
using VirtoCommerce.Webshop.Converters;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IVirtoCommerceClient _apiClient;

        public OrderService(IVirtoCommerceClient apiClient, IShoppingCartService shoppingCartService)
        {
            _apiClient = apiClient;
        }

        public async Task<Order> CreateOrderAsync(string shoppingCartId)
        {
            var apiResponse = await _apiClient.OrderClient.CreateOrderAsync(shoppingCartId);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.ToViewModel();
        }

        public async Task<Order> GetOrderAsync(string customerId, string orderId)
        {
            var apiResponse = await _apiClient.OrderClient.GetCustomerOrderAsync(customerId, orderId).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.ToViewModel();
        }

        public async Task<PaymentResult> ProcessPaymentAsync(string orderId, string paymentId)
        {
            var apiResponse = await _apiClient.OrderClient.ProcessPayment(orderId, paymentId, null).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.ToViewModel();
        }

        public async Task<PaymentResult> ProcessPaymentAsync(ICollection<KeyValuePair<string, string>> parameters)
        {
            var apiResponse = await _apiClient.OrderClient.PostPaymentProcess(parameters).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.ToViewModel();
        }
    }
}