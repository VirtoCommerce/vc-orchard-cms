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

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<string> Images { get; set; }

        public string UrlPattern { get; set; }
    }
}