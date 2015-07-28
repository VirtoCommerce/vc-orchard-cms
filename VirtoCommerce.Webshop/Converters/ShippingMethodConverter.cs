using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class ShippingMethodConverter
    {
        public static ShippingMethod ToViewModel(this DataContracts.Cart.ShipmentMethod shippingMethod)
        {
            var shippingMethodModel = new ShippingMethod();

            shippingMethodModel.Currency = shippingMethod.Currency;
            shippingMethodModel.ImageUrl = shippingMethod.LogoUrl;
            shippingMethodModel.Keyword = shippingMethod.ShipmentMethodCode;
            shippingMethodModel.Name = shippingMethod.Name;
            shippingMethodModel.Price = shippingMethod.Price;

            return shippingMethodModel;
        }
    }
}