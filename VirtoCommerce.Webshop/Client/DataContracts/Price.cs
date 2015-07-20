namespace VirtoCommerce.Webshop.Client.DataContracts
{
    public class Price
    {
        public decimal List { get; set; }

        public int MinQuantity { get; set; }

        public string PricelistId { get; set; }

        public string ProductId { get; set; }

        public decimal? Sale { get; set; }
    }
}