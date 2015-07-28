namespace VirtoCommerce.Webshop.ViewModels
{
    public class PaymentResult
    {
        public string PaymentStatus { get; set; }

        public bool IsSuccess { get; set; }

        public string Error { get; set; }

        public string RedirectUrl { get; set; }

        public string PaymentType { get; set; }

        public string HtmlForm { get; set; }

        public string OuterId { get; set; }
    }
}