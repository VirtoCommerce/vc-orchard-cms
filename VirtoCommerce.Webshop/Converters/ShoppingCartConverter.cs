using System.Collections.Generic;
using System.Linq;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class ShoppingCartConverter
    {
        public static ShoppingCart ToViewModel(this DataContracts.Cart.ShoppingCart shoppingCart)
        {
            var shoppingCartModel = new ShoppingCart();

            shoppingCartModel.Culture = shoppingCart.LanguageCode;
            shoppingCartModel.Currency = shoppingCart.Currency;
            shoppingCartModel.CustomerId = shoppingCart.CustomerId;
            shoppingCartModel.Id = shoppingCart.Id;

            foreach (var lineItem in shoppingCart.Items)
            {
                shoppingCartModel.LineItems.Add(lineItem.ToViewModel());
            }

            shoppingCartModel.StoreId = shoppingCart.StoreId;

            return shoppingCartModel;
        }

        public static Checkout ToViewModel(this DataContracts.Cart.ShoppingCart shoppingCart, IEnumerable<PaymentMethod> paymentMethods, IEnumerable<ShippingMethod> shippingMethods)
        {
            var checkoutModel = new Checkout();

            if (shoppingCart.Addresses != null)
            {
                var address = shoppingCart.Addresses.FirstOrDefault();
                if (address != null)
                {
                    checkoutModel.BillingAddress = address.ToViewModel();
                    checkoutModel.ShippingAddress = address.ToViewModel();
                }
            }

            checkoutModel.Culture = shoppingCart.LanguageCode;
            checkoutModel.CustomerId = shoppingCart.CustomerId;
            checkoutModel.Currency = shoppingCart.Currency;
            checkoutModel.Id = shoppingCart.Id;

            foreach (var lineItem in shoppingCart.Items)
            {
                checkoutModel.LineItems.Add(lineItem.ToViewModel());
            }

            if (shoppingCart.Payments != null)
            {
                var firstPayment = shoppingCart.Payments.FirstOrDefault();
                if (firstPayment != null)
                {
                    checkoutModel.PaymentMethodId = firstPayment.PaymentGatewayCode;
                }
            }

            foreach (var paymentMethod in paymentMethods)
            {
                checkoutModel.PaymentMethods.Add(paymentMethod);
            }

            if (shoppingCart.Shipments != null)
            {
                var firstShipment = shoppingCart.Shipments.FirstOrDefault();
                if (firstShipment != null)
                {
                    checkoutModel.ShippingMethodId = firstShipment.ShipmentMethodCode;
                }
            }

            foreach (var shippingMethod in shippingMethods)
            {
                checkoutModel.ShippingMethods.Add(shippingMethod);
            }

            checkoutModel.StoreId = shoppingCart.StoreId;

            return checkoutModel;
        }

        public static DataContracts.Cart.ShoppingCart ToApiModel(this ShoppingCart shoppingCart)
        {
            var shoppingCartModel = new DataContracts.Cart.ShoppingCart();

            shoppingCartModel.Currency = shoppingCart.Currency;
            shoppingCartModel.CustomerId = shoppingCart.CustomerId;
            shoppingCartModel.Id = shoppingCart.Id;

            shoppingCartModel.Items = new List<DataContracts.Cart.CartItem>();
            foreach (var lineItem in shoppingCart.LineItems)
            {
                shoppingCartModel.Items.Add(lineItem.ToApiModel());
            }

            shoppingCartModel.LanguageCode = shoppingCart.Culture;
            shoppingCartModel.Name = shoppingCart.Name;
            shoppingCartModel.StoreId = shoppingCart.StoreId;
            shoppingCartModel.SubTotal = shoppingCart.Subtotal;
            shoppingCartModel.Total = shoppingCart.Total;

            return shoppingCartModel;
        }

        public static DataContracts.Cart.ShoppingCart ToApiModel(this Checkout checkout)
        {
            var shoppingCart = (ShoppingCart)checkout;

            var shoppingCartModel = shoppingCart.ToApiModel();

            if (checkout.BillingAddress != null)
            {
                if (shoppingCartModel.Addresses == null)
                {
                    shoppingCartModel.Addresses = new List<DataContracts.Cart.Address>();
                }

                var billingAddress = checkout.BillingAddress.ToApiModel();
                billingAddress.Type = DataContracts.Cart.AddressType.Billing;

                shoppingCartModel.Addresses.Add(billingAddress);
            }

            if (checkout.ShippingAddress != null)
            {
                if (shoppingCartModel.Addresses == null)
                {
                    shoppingCartModel.Addresses = new List<DataContracts.Cart.Address>();
                }
                var shippingAddress = checkout.ShippingAddress.ToApiModel();
                shippingAddress.Type = DataContracts.Cart.AddressType.Shipping;

                shoppingCartModel.Addresses.Add(shippingAddress);
            }

            if (!string.IsNullOrEmpty(checkout.PaymentMethodId))
            {
                shoppingCartModel.Payments = new List<DataContracts.Cart.Payment>();
                shoppingCartModel.Payments.Add(new DataContracts.Cart.Payment
                {
                    Amount = checkout.Total,
                    Currency = checkout.Currency,
                    PaymentGatewayCode = checkout.PaymentMethodId
                });
            }

            if (!string.IsNullOrEmpty(checkout.ShippingMethodId))
            {
                shoppingCartModel.Shipments = new List<DataContracts.Cart.Shipment>();
                shoppingCartModel.Shipments.Add(new DataContracts.Cart.Shipment
                {
                    Currency = checkout.Currency,
                    ShipmentMethodCode = checkout.ShippingMethodId,
                    ShippingPrice = checkout.ShippingPrice
                });

                shoppingCartModel.ShippingTotal = checkout.ShippingPrice;
            }

            shoppingCartModel.SubTotal = checkout.Subtotal;
            shoppingCartModel.Total = checkout.Total;

            return shoppingCartModel;
        }
    }
}