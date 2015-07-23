using System.Collections.Generic;

namespace VirtoCommerce.Webshop.Client.DataContracts.Order
{
    public class CustomerOrder
    {
        public string Id { get; set; }

        public string CustomerName { get; set; }

        public string CustomerId { get; set; }

        public string ChannelId { get; set; }

        public string StoreId { get; set; }

        public string StoreName { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationId { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeId { get; set; }

        public decimal DiscountAmount { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<PaymentIn> InPayments { get; set; }

        public ICollection<LineItem> Items { get; set; }

        public ICollection<Shipment> Shipments { get; set; }

        public Discount Discount { get; set; }

        public ICollection<TaxDetail> TaxDetails { get; set; }
    }
}