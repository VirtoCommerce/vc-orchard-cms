using System.Collections.Generic;

namespace VirtoCommerce.Webshop.ViewModels
{
    public class Product
    {
        public Product()
        {
            Images = new List<string>();
        }

        public string Id { get; set; }

        public string Slug { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public Price Price { get; set; }

        public string ImageUrl { get; set; }

        public string CatalogId { get; set; }

        public string CategoryId { get; set; }

        public string Description { get; set; }

        public ICollection<string> Images { get; private set; }

        public string UrlPattern
        {
            get
            {
                return "~/Product?id={0}";
            }
        }
    }
}