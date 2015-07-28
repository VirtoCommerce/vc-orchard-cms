using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class PaymentMethodConverter
    {
        public static PaymentMethod ToViewModel(this DataContracts.Cart.PaymentMethod paymentMethod)
        {
            var paymentMethodModel = new PaymentMethod();

            paymentMethodModel.Description = paymentMethod.Description;
            paymentMethodModel.ImageUrl = paymentMethod.IconUrl;
            paymentMethodModel.Keyword = paymentMethod.GatewayCode;
            paymentMethodModel.Name = paymentMethod.Name;
            paymentMethodModel.Type = paymentMethod.Type;

            return paymentMethodModel;
        }
    }
}