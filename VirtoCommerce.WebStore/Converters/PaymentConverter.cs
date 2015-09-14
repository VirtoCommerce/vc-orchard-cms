using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class PaymentConverter
    {
        public static PaymentResult ToViewModel(this DataContracts.PostProcessPaymentResult paymentResult)
        {
            var paymentResultViewModel = new PaymentResult();

            paymentResultViewModel.Error = paymentResult.Error;
            paymentResultViewModel.IsSuccess = paymentResult.IsSuccess;
            paymentResultViewModel.RedirectUrl = paymentResult.ReturnUrl;
            paymentResultViewModel.PaymentStatus = paymentResult.NewPaymentStatus.ToString();

            return paymentResultViewModel;
        }

        public static PaymentResult ToViewModel(this DataContracts.ProcessPaymentResult paymentResult)
        {
            var paymentResultViewModel = new PaymentResult();

            paymentResultViewModel.Error = paymentResult.Error;
            paymentResultViewModel.HtmlForm = paymentResult.HtmlForm;
            paymentResultViewModel.IsSuccess = paymentResult.IsSuccess;
            paymentResultViewModel.OuterId = paymentResult.OuterId;
            paymentResultViewModel.PaymentStatus = paymentResult.NewPaymentStatus.ToString();
            paymentResultViewModel.PaymentType = paymentResult.PaymentMethodType.ToString();
            paymentResultViewModel.RedirectUrl = paymentResult.RedirectUrl;

            return paymentResultViewModel;
        }
    }
}