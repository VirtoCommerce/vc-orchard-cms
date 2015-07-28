namespace VirtoCommerce.Webshop.ViewModels
{
    public class Price
    {
        public decimal Original { get; set; }

        public decimal? Sale { get; set; }

        public string ProductId { get; set; }

        public string PricelistId { get; set; }
    }
}