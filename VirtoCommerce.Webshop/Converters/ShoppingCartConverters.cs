using DataContracts = VirtoCommerce.Webshop.Client.DataContracts;
using VirtoCommerce.Webshop.ViewModels;
using System.Collections.Generic;

namespace VirtoCommerce.Webshop.Converters
{
    public static class ShoppingCartConverters
    {
        public static ShoppingCart ToViewModel(this DataContracts.Cart.ShoppingCart shoppingCart)
        {
            var shoppingCartModel = new ShoppingCart();

            shoppingCartModel.Currency = shoppingCart.Currency;
            shoppingCartModel.CustomerId = shoppingCart.CustomerId;
            shoppingCartModel.Id = shoppingCart.Id;

            foreach (var lineItem in shoppingCart.Items)
            {
                shoppingCartModel.LineItems.Add(lineItem.ToViewModel());
            }

            shoppingCartModel.Name = shoppingCart.Name;
            shoppingCartModel.StoreId = shoppingCart.StoreId;

            return shoppingCartModel;
        }

        public static LineItem ToViewModel(this DataContracts.Cart.LineItem lineItem)
        {
            var lineItemModel = new LineItem();

            lineItemModel.Id = lineItem.Id;
            lineItemModel.ImageUrl = lineItem.ImageUrl;
            lineItemModel.Name = lineItem.Name;

            lineItemModel.Price = new Price
            {
                Actual = lineItem.PlacedPrice,
                Original = lineItem.ListPrice,
                Sale = lineItem.SalePrice,
                ProductId = lineItem.ProductId
            };

            lineItemModel.ProductId = lineItem.ProductId;
            lineItemModel.Quantity = lineItem.Quantity;
            lineItemModel.Sku = lineItem.ProductCode;

            return lineItemModel;
        }

        public static DataContracts.Cart.ShoppingCart ToApiModel(this ShoppingCart shoppingCart)
        {
            var shoppingCartModel = new DataContracts.Cart.ShoppingCart();

            shoppingCartModel.Currency = shoppingCart.Currency;
            shoppingCartModel.CustomerId = shoppingCart.CustomerId;
            shoppingCartModel.Id = shoppingCart.Id;

            shoppingCartModel.Items = new List<DataContracts.Cart.LineItem>();
            foreach (var lineItem in shoppingCart.LineItems)
            {
                shoppingCartModel.Items.Add(lineItem.ToApiModel());
            }

            shoppingCartModel.Name = shoppingCart.Name;
            shoppingCartModel.StoreId = shoppingCart.StoreId;

            return shoppingCartModel;
        }

        public static DataContracts.Cart.LineItem ToApiModel(this LineItem lineItem)
        {
            var lineItemModel = new DataContracts.Cart.LineItem();

            lineItemModel.ExtendedPrice = lineItem.LinePrice;
            lineItemModel.Id = lineItem.Id;
            lineItemModel.ListPrice = lineItem.Price.Original;
            lineItemModel.Name = lineItem.Name;
            lineItemModel.PlacedPrice = lineItem.Price.Actual;
            lineItemModel.ProductCode = lineItem.Sku;
            lineItemModel.ProductId = lineItem.ProductId;
            lineItemModel.SalePrice = lineItem.Price.Sale.HasValue ? lineItem.Price.Sale.Value : lineItem.Price.Original;
            lineItemModel.Quantity = lineItem.Quantity;
            lineItemModel.ImageUrl = lineItem.ImageUrl;

            return lineItemModel;
        }
    }
}