using System;
using System.Linq;
using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class ProductConverter
    {
        public static Product ToViewModel(this DataContracts.Product product, Price price)
        {
            var productViewModel = new Product();

            if (product.EditorialReviews != null)
            {
                var editorialReview = product.EditorialReviews.FirstOrDefault(er => !string.IsNullOrEmpty(er.ReviewType) && er.ReviewType.Equals("quickreview", StringComparison.OrdinalIgnoreCase));
                if (editorialReview != null)
                {
                    productViewModel.Description = editorialReview.Content;
                }
            }

            if (product.PrimaryImage != null)
            {
                productViewModel.FeaturedImage = product.PrimaryImage.ToViewModel();
            }

            productViewModel.Id = product.Id;
            productViewModel.Price = price;
            productViewModel.Sku = product.Code;

            if (product.Seo != null)
            {
                var seo = product.Seo.FirstOrDefault(s => !string.IsNullOrEmpty(s.Keyword));
                if (seo != null)
                {
                    productViewModel.Slug = seo.Keyword;
                }
            }

            productViewModel.Title = product.Name;

            return productViewModel;
        }
    }
}