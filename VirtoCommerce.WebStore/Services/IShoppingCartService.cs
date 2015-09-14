using Orchard;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Services
{
    public interface IShoppingCartService : IDependency
    {
        Task CreateCartAsync(string storeId, string customerId);

        Task<Cart> GetCartAsync(string storeId, string customerId);

        Task<Checkout> GetCheckoutAsync(string storeId, string customerId);

        Task UpdateCartAsync(Cart cart);

        Task UpdateCartAsync(Checkout checkout);

        Task DeleteCartAsync(string cartId);
    }
}