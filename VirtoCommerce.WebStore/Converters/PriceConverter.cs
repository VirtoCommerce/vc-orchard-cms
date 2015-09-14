using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class PriceConverter
    {
        public static Price ToViewModel(this DataContracts.Price price)
        {
            var priceViewModel = new Price();

            priceViewModel.Original = price.List;
            priceViewModel.PricelistId = price.PricelistId;
            priceViewModel.ProductId = price.ProductId;

            return priceViewModel;
        }
    }
}