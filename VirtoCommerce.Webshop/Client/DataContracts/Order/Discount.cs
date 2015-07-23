namespace VirtoCommerce.Webshop.Client.DataContracts.Order
{
    public class Discount
    {
        public string PromotionId { get; set; }

        public string Currency { get; set; }

        public decimal DiscountAmount { get; set; }

        public Coupon Coupon { get; set; }

        public string Description { get; set; }
    }
}