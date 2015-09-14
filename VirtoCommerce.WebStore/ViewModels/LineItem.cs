namespace VirtoCommerce.WebStore.ViewModels
{
    public class LineItem
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public string CatalogId { get; set; }

        public string CategoryId { get; set; }

        public string Sku { get; set; }

        public string Title { get; set; }

        public Price Price { get; set; }

        public decimal LinePrice
        {
            get
            {
                return Price.Original * Quantity;
            }
        }

        public int Quantity { get; set; }

        public Image Image { get; set; }
    }
}