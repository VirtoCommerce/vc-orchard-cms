using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using VirtoCommerce.Webshop.Models;

namespace VirtoCommerce.Webshop.Handlers
{
    public class ProductListHandler : ContentHandler
    {
        public ProductListHandler(IRepository<ProductListPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}