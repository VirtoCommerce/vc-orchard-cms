using System;

namespace VirtoCommerce.Webshop.Client.DataContracts.Order
{
    public class PaymentIn
    {
        public string Id { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerId { get; set; }

        public string Purpose { get; set; }

        public string GatewayCode { get; set; }

        public DateTime? IncomingDate { get; set; }

        public string OuterId { get; set; }
    }
}