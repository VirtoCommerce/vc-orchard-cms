using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class PaymentConverter
    {
        public static PaymentResult ToViewModel(this DataContracts.PostProcessPaymentResult paymentResult)
        {
            var paymentResultModel = new PaymentResult();

            paymentResultModel.Error = paymentResult.Error;
            paymentResultModel.IsSuccess = paymentResult.IsSuccess;
            paymentResultModel.RedirectUrl = paymentResult.ReturnUrl;
            paymentResultModel.PaymentStatus = paymentResult.NewPaymentStatus.ToString();

            return paymentResultModel;
        }

        public static PaymentResult ToViewModel(this DataContracts.ProcessPaymentResult paymentResult)
        {
            var paymentResultModel = new PaymentResult();

            paymentResultModel.Error = paymentResult.Error;
            paymentResultModel.HtmlForm = paymentResult.HtmlForm;
            paymentResultModel.IsSuccess = paymentResult.IsSuccess;
            paymentResultModel.OuterId = paymentResult.OuterId;
            paymentResultModel.PaymentStatus = paymentResult.NewPaymentStatus.ToString();
            paymentResultModel.PaymentType = paymentResult.PaymentMethodType.ToString();
            paymentResultModel.RedirectUrl = paymentResult.RedirectUrl;

            return paymentResultModel;
        }
    }
}