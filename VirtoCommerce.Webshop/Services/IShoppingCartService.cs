using Orchard;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public interface IShoppingCartService : IDependency
    {
        Task<ShoppingCart> GetShoppingCartAsync(string storeId, string customerId);

        Task CreateShoppingCartAsync(ShoppingCart shoppingCart);

        Task UpdateShoppingCartAsync(ShoppingCart shoppingCart);

        Task UpdateCheckoutAsync(Checkout checkout);

        Task DeleteShoppingCartAsync(string id);

        Task<IEnumerable<ShippingMethod>> GetShippingMethodsAsync(string shoppingcartId);

        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync(string shoppingCartId);

        Task<Checkout> GetCheckoutAsync(string storeId, string customerId);
    }
}