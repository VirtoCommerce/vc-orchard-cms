using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class PaymentMethodConverter
    {
        public static PaymentMethod ToViewModel(this DataContracts.Cart.PaymentMethod paymentMethod)
        {
            var paymentMethodViewModel = new PaymentMethod();

            paymentMethodViewModel.Description = paymentMethod.Description;
            paymentMethodViewModel.ImageUrl = paymentMethod.IconUrl;
            paymentMethodViewModel.Keyword = paymentMethod.GatewayCode;
            paymentMethodViewModel.Title = paymentMethod.Name;
            paymentMethodViewModel.Type = paymentMethod.Type;

            return paymentMethodViewModel;
        }
    }
}