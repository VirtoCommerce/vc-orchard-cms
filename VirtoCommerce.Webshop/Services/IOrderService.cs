using Orchard;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public interface IOrderService : IDependency
    {
        Task<Order> CreateOrderAsync(string shoppingCartId);

        Task<Order> GetOrderAsync(string customerId, string orderNumber);

        Task<PaymentResult> ProcessPaymentAsync(string orderId, string paymentId);

        Task<PaymentResult> ProcessPaymentAsync(ICollection<KeyValuePair<string, string>> parameters);
    }
}