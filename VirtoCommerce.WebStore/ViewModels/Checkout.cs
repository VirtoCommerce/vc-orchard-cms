using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VirtoCommerce.WebStore.ViewModels
{
    public class Checkout
    {
        public Checkout()
        {
            PaymentMethods = new List<PaymentMethod>();
            ShippingMethods = new List<ShippingMethod>();
            LineItems = new List<LineItem>();
        }

        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Address BillingAddress { get; set; }

        public Address ShippingAddress { get; set; }

        [Required]
        public string ShippingMethodId { get; set; }

        [Required]
        public string PaymentMethodId { get; set; }

        public ICollection<LineItem> LineItems { get; private set; }

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