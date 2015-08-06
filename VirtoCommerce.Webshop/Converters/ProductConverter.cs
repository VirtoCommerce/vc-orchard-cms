using System;
using System.Linq;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class ProductConverter
    {
        public static Product ToViewModel(this DataContracts.Product product, Price priceModel)
        {
            var productModel = new Product();

            productModel.CatalogId = product.CatalogId;
            productModel.CategoryId = "fake";

            if (product.EditorialReviews != null)
            {
                var description = product.EditorialReviews.FirstOrDefault(er =>
                    !string.IsNullOrEmpty(er.Content) &&
                    !string.IsNullOrEmpty(er.ReviewType) &&
                    er.ReviewType.Equals("quickreview", StringComparison.OrdinalIgnoreCase));
                if (description != null)
                {
                    productModel.Description = description.Content;
                }
            }

            productModel.Id = product.Id;

            if (product.Images != null)
            {
                foreach (var image in product.Images.Skip(0).Take(3))
                {
                    productModel.Images.Add(image.Src);
                }
            }

            productModel.ImageUrl = product.PrimaryImage != null ? product.PrimaryImage.Src : null;
            productModel.Name = product.Name;
            productModel.Price = priceModel;
            productModel.Sku = product.Code;
            productModel.Slug = product.Code;

            if (product.Seo != null)
            {
                var seo = product.Seo.FirstOrDefault(s => !string.IsNullOrEmpty(s.Keyword));
                if (seo != null)
                {
                    productModel.Slug = seo.Keyword;
                }
            }

            return productModel;
        }

        public static LineItem ToLineItem(this Product product, int quantity, string currency)
        {
            var lineItem = new LineItem();

            lineItem.CatalogId = product.CatalogId;
            lineItem.CategoryId = product.CategoryId;
            lineItem.Currency = currency;
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