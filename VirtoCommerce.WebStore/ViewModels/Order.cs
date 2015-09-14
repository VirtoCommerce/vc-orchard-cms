using System.Collections.Generic;
using System.Linq;

namespace VirtoCommerce.WebStore.ViewModels
{
    public class Order
    {
        public Order()
        {
            LineItems = new List<LineItem>();
        }

        public string Id { get; set; }

        public string Number { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<LineItem> LineItems { get; private set; }

        public string PaymentId { get; set; }

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
                decimal shippingPrice = ShippingMethod != null ? ShippingMethod.Price : 0M;

                return Subtotal + shippingPrice;
            }
        }
    }
}