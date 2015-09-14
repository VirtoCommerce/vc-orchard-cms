using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ApiClient;
using VirtoCommerce.WebStore.Converters;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IVirtoCommerceClient _apiClient;

        public OrderService(IVirtoCommerceClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Order> CreateOrderAsync(string cartId)
        {
            var apiResponse = await _apiClient.OrderClient.CreateOrderAsync(cartId).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var orderViewModel = apiResponse.ToViewModel();

            return orderViewModel;
        }

        public async Task<Order> GetOrderAsync(string customerId, string orderId)
        {
            var apiResponse = await _apiClient.OrderClient.GetCustomerOrderAsync(customerId, orderId).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var orderViewModel = apiResponse.ToViewModel();

            return orderViewModel;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(ICollection<KeyValuePair<string, string>> parameters)
        {
            var apiResponse = await _apiClient.OrderClient.PostPaymentProcess(parameters).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var paymentResultViewModel = apiResponse.ToViewModel();

            return paymentResultViewModel;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(string orderId, string paymentId)
        {
            var apiResponse = await _apiClient.OrderClient.ProcessPayment(orderId, paymentId, null).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            var paymentResultViewModel = apiResponse.ToViewModel();

            return paymentResultViewModel;
        }
    }
}