using Orchard;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Services
{
    public interface IStoreService : IDependency
    {
        Task<Store> GetStoreAsync(string storeId);
    }
}