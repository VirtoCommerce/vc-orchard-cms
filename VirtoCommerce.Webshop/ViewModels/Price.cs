namespace VirtoCommerce.Webshop.ViewModels
{
    public class Price
    {
        public string PricelistId { get; set; }

        public string ProductId { get; set; }

        public decimal Original { get; set; }

        public decimal? Sale { get; set; }
    }
}