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

            productModel.Price = price;
            productModel.Sku = product.Code;
            productModel.Slug = product.Code;

            if (product.Seo != null)
            {
                var seo = product.Seo.FirstOrDefault(s => !String.IsNullOrEmpty(s.Keyword));
                if (seo != null)
                {
                    productModel.Slug = seo.Keyword;
                }
            }

            productModel.UrlPattern = "~/Product?slug={0}";

            return productModel;
        }

        public static LineItem ToLineItem(this Product product, int quantity)
        {
            var lineItem = new LineItem();

            lineItem.ImageUrl = product.ImageUrl;
            lineItem.Name = product.Name;
            lineItem.Price = product.Price;
            lineItem.ProductId = product.Id;
            lineItem.Quantity = quantity;
            lineItem.Sku = product.Sku;

            return lineItem;
        }
    }
}