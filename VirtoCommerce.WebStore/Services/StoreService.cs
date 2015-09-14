using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ApiClient;
using VirtoCommerce.WebStore.Converters;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Services
{
    public class StoreService : IStoreService
    {
        private readonly IVirtoCommerceClient _apiClient;

        public StoreService(IVirtoCommerceClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Store> GetStoreAsync(string storeId)
        {
            var stores = await _apiClient.StoreClient.GetStoresAsync().ConfigureAwait(false);

            if (stores == null)
            {
                return null;
            }

            var store = stores.FirstOrDefault(s => s.Id == storeId);

            if (store == null)
            {
                return null;
            }

            var storeViewModel = store.ToViewModel();

            return storeViewModel;
        }
    }
}