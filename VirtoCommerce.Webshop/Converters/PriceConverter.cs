using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class PriceConverter
    {
        public static Price ToViewModel(this DataContracts.Price price)
        {
            var priceModel = new Price();

            priceModel.PricelistId = price.PricelistId;
            priceModel.Original = price.List;
            priceModel.ProductId = price.ProductId;
            priceModel.Sale = price.Sale;

            return priceModel;
        }
    }
}