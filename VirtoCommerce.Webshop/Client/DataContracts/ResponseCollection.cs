using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VirtoCommerce.Webshop.Client.DataContracts
{
    public class ResponseCollection<T>
    {
        private ICollection<T> _items;
        public ICollection<T> Items
        {
            get { return _items ?? (_items = new Collection<T>()); }
        }

        public int Total { get; set; }
    }
}