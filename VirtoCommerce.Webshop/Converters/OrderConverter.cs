using System.Linq;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;
using System.Collections.Generic;

namespace VirtoCommerce.Webshop.Converters
{
    public static class OrderConverter
    {
        public static Order ToViewModel(this DataContracts.Orders.CustomerOrder order)
        {
            var orderModel = new Order();

            if (order.Addresses != null)
            {
                var billingAddress = order.Addresses.FirstOrDefault(a => a.AddressType == DataContracts.Orders.AddressType.Billing);
                if (billingAddress != null)
                {
                    orderModel.BillingAddress = billingAddress.ToViewModel();
                }

                var shippingAddress = order.Addresses.FirstOrDefault(a => a.AddressType == DataContracts.Orders.AddressType.Shipping);
                if (shippingAddress != null)
                {
                    orderModel.ShippingAddress = shippingAddress.ToViewModel();
                }
            }

            orderModel.Id = order.Id;

            foreach (var lineItem in order.Items)
            {
                orderModel.LineItems.Add(lineItem.ToViewModel());
            }

            orderModel.OrderNumber = order.Number;

            if (order.InPayments != null)
            {
                var firstPayment = order.InPayments.FirstOrDefault();
                if (firstPayment != null)
                {
                    orderModel.FinancialStatus = firstPayment.Status;
                    if (string.IsNullOrEmpty(firstPayment.Status))
                    {
                        if (firstPayment.IsCancelled)
                        {
                            orderModel.FinancialStatus = "Cancelled";
                        }
                        else
                        {
                            orderModel.FinancialStatus = firstPayment.IsApproved ? "Paid" : "Pending";
                        }
                    }

                    orderModel.PaymentId = firstPayment.Id;
                    orderModel.PaymentMethodId = firstPayment.GatewayCode;
                }
            }

            if (order.Shipments != null)
            {
                var firstShipment = order.Shipments.FirstOrDefault();
                if (firstShipment != null)
                {
                    orderModel.FulfillmentStatus = firstShipment.Status;
                    if (string.IsNullOrEmpty(firstShipment.Status))
                    {
                        if (firstShipment.IsCancelled)
                        {
                            orderModel.FulfillmentStatus = "Cancelled";
                        }
                        else
                        {
                            orderModel.FulfillmentStatus = firstShipment.IsApproved ? "Sent" : "Not sent";
                        }
                    }

                    orderModel.ShippingMethodId = firstShipment.ShipmentMethodCode;
                }

                orderModel.ShippingPrice = order.Shipments.Sum(s => s.Sum);
            }

            return orderModel;
        }
    }
}