using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class CheckoutConverter
    {
        public static Checkout ToViewModel(this DataContracts.Cart.ShoppingCart cart, IEnumerable<DataContracts.Cart.PaymentMethod> paymentMethods, IEnumerable<DataContracts.Cart.ShipmentMethod> shippingMethods)
        {
            var checkoutViewModel = new Checkout();

            if (cart.Addresses != null)
            {
                var address = cart.Addresses.FirstOrDefault();
                if (address != null)
                {
                    checkoutViewModel.BillingAddress = address.ToViewModel();
                    checkoutViewModel.ShippingAddress = address.ToViewModel();

                    checkoutViewModel.Email = address.Email;
                }
            }

            checkoutViewModel.Id = cart.Id;

            if (cart.Items != null)
            {
                foreach (var lineItem in cart.Items)
                {
                    checkoutViewModel.LineItems.Add(lineItem.ToViewModel());
                }
            }

            if (cart.Payments != null)
            {
                var firstPayment = cart.Payments.FirstOrDefault();
                if (firstPayment != null)
                {
                    checkoutViewModel.PaymentMethodId = firstPayment.PaymentGatewayCode;
                }
            }

            if (paymentMethods != null)
            {
                foreach (var paymentMethod in paymentMethods)
                {
                    checkoutViewModel.PaymentMethods.Add(paymentMethod.ToViewModel());
                }
            }

            if (cart.Shipments != null)
            {
                var firstShipment = cart.Shipments.FirstOrDefault();
                if (firstShipment != null)
                {
                    checkoutViewModel.ShippingMethodId = firstShipment.ShipmentMethodCode;
                }
            }

            if (shippingMethods != null)
            {
                foreach (var shippingMethod in shippingMethods)
                {
                    checkoutViewModel.ShippingMethods.Add(shippingMethod.ToViewModel());
                }
            }

            return checkoutViewModel;
        }

        public static DataContracts.Cart.ShoppingCart ToApiModel(this Checkout checkoutViewModel)
        {
            var cart = new DataContracts.Cart.ShoppingCart();

            if (checkoutViewModel.BillingAddress != null)
            {
                if (cart.Addresses == null)
                {
                    cart.Addresses = new List<DataContracts.Cart.Address>();
                }

                var address = checkoutViewModel.BillingAddress.ToApiModel();
                address.Email = checkoutViewModel.Email;
                address.Type = DataContracts.Cart.AddressType.Billing;

                cart.Addresses.Add(address);
            }

            cart.Id = checkoutViewModel.Id;

            cart.Items = new List<DataContracts.Cart.CartItem>();
            foreach (var lineItemViewModel in checkoutViewModel.LineItems)
            {
                cart.Items.Add(lineItemViewModel.ToApiModel());
            }

            if (!string.IsNullOrEmpty(checkoutViewModel.PaymentMethodId))
            {
                cart.Payments = new List<DataContracts.Cart.Payment>();
                cart.Payments.Add(new DataContracts.Cart.Payment
                {
                    Amount = checkoutViewModel.Total,
                    PaymentGatewayCode = checkoutViewModel.PaymentMethodId
                });
            }

            if (!string.IsNullOrEmpty(checkoutViewModel.ShippingMethodId))
            {
                cart.Shipments = new List<DataContracts.Cart.Shipment>();
                cart.Shipments.Add(new DataContracts.Cart.Shipment
                {
                    ShipmentMethodCode = checkoutViewModel.ShippingMethodId,
                    ShippingPrice = checkoutViewModel.ShippingPrice
                });
                cart.ShippingTotal = checkoutViewModel.ShippingPrice;
            }

            cart.SubTotal = checkoutViewModel.Subtotal;
            cart.Total = checkoutViewModel.Total;

            return cart;
        }
    }
}