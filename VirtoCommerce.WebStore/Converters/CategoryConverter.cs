using System.Linq;
using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class CategoryConverter
    {
        public static Category ToViewModel(this DataContracts.Category category)
        {
            var categoryViewModel = new Category();

            categoryViewModel.Code = category.Code;
            categoryViewModel.Id = category.Id;

            if (category.Image != null)
            {
                categoryViewModel.Image = category.Image.ToViewModel();
            }

            if (category.Seo != null)
            {
                var seo = category.Seo.FirstOrDefault(s => !string.IsNullOrEmpty(s.Keyword));
                if (seo != null)
                {
                    categoryViewModel.Slug = seo.Keyword;
                }
            }

            if (category.Parents != null)
            {
                foreach (var parentCategory in category.Parents)
                {
                    categoryViewModel.ParentCategories.Add(parentCategory.ToViewModel());
                }
            }

            categoryViewModel.Title = category.Name;

            return categoryViewModel;
        }
    }
}