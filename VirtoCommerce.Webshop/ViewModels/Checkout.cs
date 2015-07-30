using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VirtoCommerce.Webshop.ViewModels
{
    public class Checkout : ShoppingCart
    {
        public Checkout()
        {
            ShippingMethods = new List<ShippingMethod>();
            PaymentMethods = new List<PaymentMethod>();
        }

        [Required]
        public Address BillingAddress { get; set; }

        [Required]
        public Address ShippingAddress { get; set; }

        [Required]
        public string ShippingMethodId { get; set; }

        [Required]
        public string PaymentMethodId { get; set; }

        public ICollection<ShippingMethod> ShippingMethods { get; private set; }

        public ICollection<PaymentMethod> PaymentMethods { get; private set; }

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

        public string GetError()
        {
            string errorMessage = null;

            if (BillingAddress == null)
            {
                errorMessage = "Billing address is required";
            }
            if (LineItemCount <= 0)
            {
                errorMessage = "Line items quantity cannot be less or equal 0";
            }
            if (string.IsNullOrEmpty(PaymentMethodId))
            {
                errorMessage = "Payment method is required";
            }
            if (ShippingAddress == null)
            {
                errorMessage = "Shipping address is required";
            }
            if (string.IsNullOrEmpty(ShippingMethodId))
            {
                errorMessage = "Shipping method is required";
            }

            return errorMessage;
        }
    }
}