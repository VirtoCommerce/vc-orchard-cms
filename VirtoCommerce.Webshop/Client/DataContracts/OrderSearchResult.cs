using System.Collections.Generic;
using VirtoCommerce.Webshop.Client.DataContracts.Order;

namespace VirtoCommerce.Webshop.Client.DataContracts
{
    public class OrderSearchResult
    {
        public ICollection<CustomerOrder> CustomerOrders { get; set; }

        public int TotalCount { get; set; }
    }
}