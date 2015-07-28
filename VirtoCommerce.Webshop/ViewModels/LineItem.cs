namespace VirtoCommerce.Webshop.ViewModels
{
    public class LineItem
    {
        public string Id { get; set; }

        public string CatalogId { get; set; }

        public string CategoryId { get; set; }

        public string ProductId { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public Price Price { get; set; }

        public decimal LinePrice
        {
            get
            {
                return Price != null ? Price.Original * Quantity : 0M;
            }
        }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public string Currency { get; set; }
    }
}