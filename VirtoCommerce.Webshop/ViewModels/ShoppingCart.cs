using System.Collections.Generic;
using System.Linq;
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

        public string StoreId { get; set; }

        public string Culture { get; set; }

        public string Name
        {
            get
            {
                return "default";
            }
        }

        public ICollection<LineItem> LineItems { get; private set; }

        public int LineItemCount
        {
            get
            {
                return LineItems.Count;
            }
        }

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

        public void Add(LineItem lineItem)
        {
            var existingLineItem = LineItems.FirstOrDefault(li => li.ProductId == lineItem.ProductId);
            if (existingLineItem != null)
            {
                existingLineItem.Quantity += lineItem.Quantity;
            }
            else
            {
                LineItems.Add(lineItem);
            }
        }

        public void Remove(string lineItemId)
        {
            var lineItem = LineItems.FirstOrDefault(li => li.Id == lineItemId);
            if (lineItem != null)
            {
                LineItems.Remove(lineItem);
            }
        }
    }
}