using DataContracts = VirtoCommerce.Webshop.Client.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class PriceConverters
    {
        public static Price ToViewModel(this DataContracts.Price price)
        {
            var priceModel = new Price();

            priceModel.Actual = price.List;
            priceModel.Original = price.List;
            priceModel.PricelistId = price.PricelistId;
            priceModel.ProductId = price.ProductId;
            priceModel.Sale = price.Sale;

            return priceModel;
        }
    }
}