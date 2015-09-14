using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class ShippingMethodConverter
    {
        public static ShippingMethod ToViewModel(this DataContracts.Cart.ShipmentMethod shipmentMethod)
        {
            var shippingMethodViewModel = new ShippingMethod();

            shippingMethodViewModel.ImageUrl = shipmentMethod.LogoUrl;
            shippingMethodViewModel.Keyword = shipmentMethod.ShipmentMethodCode;
            shippingMethodViewModel.Price = shipmentMethod.Price;
            shippingMethodViewModel.Title = shipmentMethod.Name;

            return shippingMethodViewModel;
        }
    }
}