using Orchard.ContentManagement;

namespace VirtoCommerce.Webshop.Models
{
    public class ProductListPart : ContentPart<ProductListPartRecord>
    {
        public int PageSize
        {
            get
            {
                int pageSize = Retrieve(x => x.PageSize);

                return pageSize > 0 ? pageSize : 20;
            }
            set
            {
                Store(x => x.PageSize, value);
            }
        }
    }
}