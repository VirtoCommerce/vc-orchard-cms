using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class StoreConverter
    {
        public static Store ToViewModel(this DataContracts.Stores.Store store)
        {
            var storeViewModel = new Store();

            storeViewModel.CatalogId = store.Catalog;
            storeViewModel.Currency = store.DefaultCurrency;
            storeViewModel.Id = store.Id;
            storeViewModel.Title = store.Name;

            return storeViewModel;
        }
    }
}