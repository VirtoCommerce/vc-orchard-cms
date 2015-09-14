using System.Collections.Generic;
using System.Linq;

namespace VirtoCommerce.WebStore.ViewModels
{
    public class Cart
    {
        public Cart()
        {
            LineItems = new List<LineItem>();
        }

        public string Id { get; set; }

        public string CustomerId { get; set; }

        public string StoreId { get; set; }

        public string Name
        {
            get
            {
                return "default";
            }
        }

        public ICollection<LineItem> LineItems { get; private set; }

        public decimal Subtotal
        {
            get
            {
                return LineItems.Sum(li => li.LinePrice);
            }
        }

        public decimal Total
        {
            get
            {
                return Subtotal;
            }
        }

        public int LineItemsCount
        {
            get
            {
                return LineItems.Count;
            }
        }
    }
}