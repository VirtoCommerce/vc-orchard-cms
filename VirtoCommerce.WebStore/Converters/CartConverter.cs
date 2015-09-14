using System.Collections.Generic;
using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class CartConverter
    {
        public static Cart ToViewModel(this DataContracts.Cart.ShoppingCart cart)
        {
            var cartViewModel = new Cart();

            cartViewModel.CustomerId = cart.CustomerId;
            cartViewModel.Id = cart.Id;

            foreach (var lineItem in cart.Items)
            {
                cartViewModel.LineItems.Add(lineItem.ToViewModel());
            }

            cartViewModel.StoreId = cart.StoreId;

            return cartViewModel;
        }

        public static DataContracts.Cart.ShoppingCart ToApiModel(this Cart cartViewModel)
        {
            var cart = new DataContracts.Cart.ShoppingCart();

            cart.CustomerId = cartViewModel.CustomerId;
            cart.Id = cartViewModel.Id;

            cart.Items = new List<DataContracts.Cart.CartItem>();
            foreach (var lineItemViewModel in cartViewModel.LineItems)
            {
                cart.Items.Add(lineItemViewModel.ToApiModel());
            }

            cart.Name = cartViewModel.Name;
            cart.StoreId = cartViewModel.StoreId;

            return cart;
        }
    }
}