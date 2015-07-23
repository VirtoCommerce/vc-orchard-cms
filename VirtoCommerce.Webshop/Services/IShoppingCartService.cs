using Orchard;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public interface IShoppingCartService : IDependency
    {
        Task<ShoppingCart> GetShoppingCartAsync(string storeId, string customerId);

        Task CreateShoppingCartAsync(ShoppingCart shoppingCart);

        Task UpdateShoppingCartAsync(ShoppingCart shoppingCart);

        Task DeleteShoppingCartAsync(string shoppingCartId);
    }
}