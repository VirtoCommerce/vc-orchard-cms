using System;
using DataContracts = VirtoCommerce.Webshop.Client.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class ShopConverters
    {
        public static Shop ToViewModel(this DataContracts.Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }

            var shopModel = new Shop();

            shopModel.CatalogId = store.Catalog;

            foreach (var currency in store.Currencies)
            {
                shopModel.Currencies.Add(currency);
            }

            shopModel.Currency = store.DefaultCurrency;
            shopModel.Id = store.Id;
            shopModel.Name = store.Name;

            return shopModel;
        }
    }
}