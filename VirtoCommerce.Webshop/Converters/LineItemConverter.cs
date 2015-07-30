using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class LineItemConverter
    {
        public static LineItem ToViewModel(this DataContracts.Cart.CartItem lineItem)
        {
            var lineItemModel = new LineItem();

            lineItemModel.CatalogId = lineItem.CatalogId;
            lineItemModel.CategoryId = lineItem.CategoryId;
            lineItemModel.Currency = lineItem.Currency;
            lineItemModel.Id = lineItem.Id;
            lineItemModel.ImageUrl = lineItem.ImageUrl;
            lineItemModel.Name = lineItem.Name;

            lineItemModel.Price = new Price
            {
                Original = lineItem.ListPrice,
                ProductId = lineItem.ProductId,
                Sale = lineItem.SalePrice > 0 ? (decimal?)lineItem.SalePrice : null
            };

            lineItemModel.ProductId = lineItem.ProductId;
            lineItemModel.Quantity = lineItem.Quantity;
            lineItemModel.Sku = lineItem.ProductCode;

            return lineItemModel;
        }

        public static LineItem ToViewModel(this DataContracts.Orders.LineItem lineItem)
        {
            var lineItemModel = new LineItem();

            lineItemModel.CatalogId = lineItem.CatalogId;
            lineItemModel.CategoryId = lineItem.CategoryId;
            lineItemModel.Currency = lineItem.Currency;
            lineItemModel.Id = lineItem.Id;
            lineItemModel.ImageUrl = lineItem.ImageUrl;
            lineItemModel.Name = lineItem.Name;

            lineItemModel.Price = new Price
            {
                Original = lineItem.BasePrice,
                ProductId = lineItem.ProductId,
                Sale = lineItem.Price
            };

            lineItemModel.ProductId = lineItem.ProductId;
            lineItemModel.Quantity = lineItem.Quantity;
            lineItemModel.Sku = null;

            return lineItemModel;
        }

        public static DataContracts.Cart.CartItem ToApiModel(this LineItem lineItem)
        {
            var lineItemModel = new DataContracts.Cart.CartItem();

            lineItemModel.CatalogId = lineItem.CatalogId;
            lineItemModel.CategoryId = lineItem.CategoryId;
            lineItemModel.Currency = lineItem.Currency;
            lineItemModel.Id = lineItem.Id;
            lineItemModel.ImageUrl = lineItem.ImageUrl;
            lineItemModel.Name = lineItem.Name;

            if (lineItem.Price != null)
            {
                lineItemModel.ListPrice = lineItem.Price.Original;
                lineItemModel.SalePrice = lineItem.Price.Sale.HasValue ? lineItem.Price.Sale.Value : 0M;
            }

            lineItemModel.ProductId = lineItem.ProductId;
            lineItemModel.Quantity = lineItem.Quantity;
            lineItemModel.ProductCode = lineItem.Sku;

            return lineItemModel;
        }
    }
}