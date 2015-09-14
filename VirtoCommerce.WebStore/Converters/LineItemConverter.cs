using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class LineItemConverter
    {
        public static LineItem ToViewModel(this DataContracts.Cart.CartItem lineItem)
        {
            var lineItemViewModel = new LineItem();

            lineItemViewModel.CatalogId = lineItem.CatalogId;
            lineItemViewModel.CategoryId = lineItem.CategoryId;
            lineItemViewModel.Id = lineItem.Id;
            lineItemViewModel.Image = new Image { Url = lineItem.ImageUrl };
            lineItemViewModel.Price = new Price { Original = lineItem.ListPrice, ProductId = lineItem.ProductId };
            lineItemViewModel.ProductId = lineItem.ProductId;
            lineItemViewModel.Quantity = lineItem.Quantity;
            lineItemViewModel.Sku = lineItem.ProductCode;
            lineItemViewModel.Title = lineItem.Name;

            return lineItemViewModel;
        }

        public static LineItem ToViewModel(this DataContracts.Orders.LineItem lineItem)
        {
            var lineItemViewModel = new LineItem();

            lineItemViewModel.CatalogId = lineItem.CatalogId;
            lineItemViewModel.CategoryId = lineItem.CategoryId;
            lineItemViewModel.Id = lineItem.Id;
            lineItemViewModel.Image = new Image { Url = lineItem.ImageUrl };
            lineItemViewModel.Price = new Price { Original = lineItem.BasePrice, ProductId = lineItem.ProductId };
            lineItemViewModel.ProductId = lineItem.ProductId;
            lineItemViewModel.Quantity = lineItem.Quantity;
            lineItemViewModel.Sku = lineItem.ProductId;
            lineItemViewModel.Title = lineItem.Name;

            return lineItemViewModel;
        }

        public static DataContracts.Cart.CartItem ToApiModel(this LineItem lineItemViewModel)
        {
            var lineItem = new DataContracts.Cart.CartItem();

            lineItem.CatalogId = lineItemViewModel.CatalogId;
            lineItem.CategoryId = lineItemViewModel.CategoryId;
            lineItem.Id = lineItemViewModel.Id;
            lineItem.ImageUrl = lineItemViewModel.Image.Url;
            lineItem.ListPrice = lineItemViewModel.Price.Original;
            lineItem.Quantity = lineItemViewModel.Quantity;
            lineItem.ProductCode = lineItemViewModel.Sku;
            lineItem.ProductId = lineItemViewModel.ProductId;
            lineItem.Name = lineItemViewModel.Title;

            return lineItem;
        }
    }
}