using Orchard.ContentManagement.Records;

namespace VirtoCommerce.Webshop.Models
{
    public class ProductListPartRecord : ContentPartRecord
    {
        public virtual int PageSize { get; set; }
    }
}