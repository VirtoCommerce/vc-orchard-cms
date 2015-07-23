namespace VirtoCommerce.Webshop.Client.DataContracts.Cart
{
    public class Discount
    {
        public string PromotionId { get; set; }

        public string Currency { get; set; }

        public decimal DiscountAmount { get; set; }

        public string Description { get; set; }
    }
}