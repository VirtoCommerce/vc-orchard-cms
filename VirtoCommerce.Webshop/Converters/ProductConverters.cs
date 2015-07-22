using System;
using System.Linq;
using DataContracts = VirtoCommerce.Webshop.Client.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class ProductConverters
    {
        public static Product ToViewModel(this DataContracts.Product product, Price price)
        {
            var productModel = new Product();

            if (product.EditorialReviews != null)
            {
                var editorialReview = product.EditorialReviews.FirstOrDefault(er =>
                    !String.IsNullOrEmpty(er.Content) && er.Content.Equals("quickreview", StringComparison.OrdinalIgnoreCase));
                if (editorialReview != null)
                {
                    productModel.Description = editorialReview.Content;
                }
            }

            productModel.Id = product.Id;

            if (product.Images != null)
            {
                foreach (var image in product.Images)
                {
                    productModel.Images.Add(image.Src);
                }
            }

            productModel.ImageUrl = product.PrimaryImage != null ? product.PrimaryImage.Src : null;
            productModel.Name = product.Name;

            if (price != null)
            {
                productModel.Price = price.Sale.HasValue ? price.Sale.Value : price.Original;
            }

            productModel.Slug = product.Code;

            if (product.Seo != null)
            {
                var seo = product.Seo.FirstOrDefault(s => !String.IsNullOrEmpty(s.Keyword));
                if (seo != null)
                {
                    productModel.Slug = seo.Keyword;
                }
            }

            productModel.UrlPattern = "~/Product/{0}";

            return productModel;
        }
    }
}