using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class ShopConverter
    {
        public static Shop ToViewModel(this DataContracts.Stores.Store store)
        {
            var shopModel = new Shop();

            shopModel.CatalogId = store.Catalog;
            shopModel.Currency = store.DefaultCurrency;
            shopModel.Id = store.Id;
            shopModel.Name = store.Name;

            return shopModel;
        }
    }
}