using System.Collections.Generic;
using System.Linq;

namespace VirtoCommerce.Webshop.ViewModels
{
    public class Order
    {
        public Order()
        {
            LineItems = new List<LineItem>();
        }

        public string Id { get; set; }

        public string OrderNumber { get; set; }

        public string FinancialStatus { get; set; }

        public string FulfillmentStatus { get; set; }

        public Address BillingAddress { get; set; }

        public Address ShippingAddress { get; set; }

        public ICollection<LineItem> LineItems { get; private set; }

        public string PaymentId { get; set; }

        public string PaymentMethodId { get; set; }

        public string ShippingMethodId { get; set; }

        public decimal Subtotal
        {
            get
            {
                return LineItems.Sum(li => li.LinePrice);
            }
        }

        public decimal ShippingPrice { get; set; }

        public decimal Total
        {
            get
            {
                return Subtotal + ShippingPrice;
            }
        }
    }
}