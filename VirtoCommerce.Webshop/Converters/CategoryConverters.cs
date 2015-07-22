using System;
using System.Linq;
using DataContracts = VirtoCommerce.Webshop.Client.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class CategoryConverters
    {
        public static Category ToViewModel(this DataContracts.Category category)
        {
            var categoryModel = new Category();

            categoryModel.Id = category.Id;
            categoryModel.Name = category.Name;
            categoryModel.Slug = category.Code;

            if (category.Seo != null)
            {
                var seo = category.Seo.FirstOrDefault(s => !String.IsNullOrEmpty(s.Keyword));
                if (seo != null)
                {
                    categoryModel.Slug = seo.Keyword;
                }
            }

            categoryModel.UrlPattern = "~/Category?id={0}";

            return categoryModel;
        }
    }
}