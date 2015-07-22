using System.Collections.Generic;

namespace VirtoCommerce.Webshop.ViewModels
{
    public class Shop
    {
        public Shop()
        {
            Currencies = new List<string>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string CatalogId { get; set; }

        public string Currency { get; set; }

        public ICollection<string> Currencies { get; private set; }
    }
}