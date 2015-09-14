using System.Collections.Generic;
using System.Web;

namespace VirtoCommerce.WebStore.ViewModels
{
    public class Category
    {
        public Category()
        {
            ParentCategories = new List<Category>();
        }

        public string Id { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Url
        {
            get
            {
                return VirtualPathUtility.ToAbsolute("~/category/" + Slug);
            }
        }

        public Image Image { get; set; }

        public ICollection<Category> ParentCategories { get; private set; }
    }
}