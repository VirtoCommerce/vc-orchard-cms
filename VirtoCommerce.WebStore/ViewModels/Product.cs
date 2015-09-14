namespace VirtoCommerce.WebStore.ViewModels
{
    public class Product
    {
        public string Id { get; set; }

        public Price Price { get; set; }

        public string Description { get; set; }

        public Image FeaturedImage { get; set; }

        public string Slug { get; set; }

        public string Sku { get; set; }

        public string Title { get; set; }
    }
}