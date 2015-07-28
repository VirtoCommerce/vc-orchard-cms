using System.Linq;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class CategoryConverter
    {
        public static Category ToViewModel(this DataContracts.Category category)
        {
            var categoryModel = new Category();

            categoryModel.Id = category.Id;
            categoryModel.Name = category.Name;
            categoryModel.Slug = category.Code;

            if (category.Seo != null)
            {
                var seo = category.Seo.FirstOrDefault(s => !string.IsNullOrEmpty(s.Keyword));
                if (seo != null)
                {
                    categoryModel.Slug = seo.Keyword;
                }
            }

            return categoryModel;
        }
    }
}