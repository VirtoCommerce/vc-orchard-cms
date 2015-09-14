using System.Linq;
using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class OrderConverter
    {
        public static Order ToViewModel(this DataContracts.Orders.CustomerOrder order)
        {
            var orderViewModel = new Order();

            orderViewModel.Id = order.Id;

            foreach (var lineItem in order.Items)
            {
                orderViewModel.LineItems.Add(lineItem.ToViewModel());
            }

            orderViewModel.Number = order.Number;

            if (order.InPayments != null)
            {
                var firstPayment = order.InPayments.FirstOrDefault();
                if (firstPayment != null)
                {
                    orderViewModel.PaymentMethod = new PaymentMethod { Keyword = firstPayment.GatewayCode };
                }

                orderViewModel.PaymentId = firstPayment.Id;
            }

            if (order.Shipments != null)
            {
                var firstShipment = order.Shipments.FirstOrDefault();
                if (firstShipment != null)
                {
                    orderViewModel.ShippingMethod = new ShippingMethod { Keyword = firstShipment.ShipmentMethodCode, Price = firstShipment.Sum };
                }
            }

            return orderViewModel;
        }
    }
}