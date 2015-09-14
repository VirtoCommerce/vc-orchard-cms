using Orchard;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Services
{
    public interface IOrderService : IDependency
    {
        Task<Order> CreateOrderAsync(string cartId);

        Task<Order> GetOrderAsync(string customerId, string orderId);

        Task<PaymentResult> ProcessPaymentAsync(string orderId, string paymentId);

        Task<PaymentResult> ProcessPaymentAsync(ICollection<KeyValuePair<string, string>> parameters);
    }
}