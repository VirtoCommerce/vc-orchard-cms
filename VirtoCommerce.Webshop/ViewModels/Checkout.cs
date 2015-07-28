using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VirtoCommerce.Webshop.ViewModels
{
    public class Checkout
    {
        public Checkout()
        {
            ShippingAddress = new Address();
            LineItems = new List<LineItem>();
            ShippingMethods = new List<ShippingMethod>();
            PaymentMethods = new List<PaymentMethod>();
        }

        public string Id { get; set; }

        public string CustomerId { get; set; }

        public string Currency { get; set; }

        public string Culture { get; set; }

        public string StoreId { get; set; }

        public string Name { get; set; }

        public Address BillingAddress { get; set; }

        public Address ShippingAddress { get; set; }

        public ICollection<LineItem> LineItems { get; private set; }

        [Required]
        public string ShippingMethodId { get; set; }

        [Required]
        public string PaymentMethodId { get; set; }

        public ICollection<ShippingMethod> ShippingMethods { get; private set; }

        public ICollection<PaymentMethod> PaymentMethods { get; private set; }

        public decimal Subtotal
        {
            get
            {
                return LineItems.Sum(li => li.LinePrice);
            }
        }

        public decimal ShippingPrice
        {
            get
            {
                decimal price = 0M;

                if (!string.IsNullOrEmpty(ShippingMethodId))
                {
                    var shippingMethod = ShippingMethods.FirstOrDefault(sm => sm.Keyword == ShippingMethodId);
                    if (shippingMethod != null)
                    {
                        price = shippingMethod.Price;
                    }
                }

                return price;
            }
        }

        public decimal Total
        {
            get
            {
                return Subtotal + ShippingPrice;
            }
        }
    }
}