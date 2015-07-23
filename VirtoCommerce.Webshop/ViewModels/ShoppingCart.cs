using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.Converters;

namespace VirtoCommerce.Webshop.ViewModels
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            LineItems = new List<LineItem>();
        }

        public string Id { get; set; }

        public string CustomerId { get; set; }

        public string Currency { get; set; }

        public string Name { get; set; }

        public string StoreId { get; set; }

        public ICollection<LineItem> LineItems { get; private set; }

        public ShoppingCart Add(Product product, int quantity)
        {
            var lineItem = LineItems.FirstOrDefault(li => li.Sku == product.Sku);
            if (lineItem != null)
            {
                lineItem.Quantity += quantity;
            }
            else
            {
                LineItems.Add(product.ToLineItem(quantity));
            }

            return this;
        }
    }
}