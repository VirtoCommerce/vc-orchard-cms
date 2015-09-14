using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class AddressConverter
    {
        public static Address ToViewModel(this DataContracts.Cart.Address address)
        {
            var addressViewModel = new Address();

            addressViewModel.City = address.City;
            addressViewModel.Company = address.Organization;
            addressViewModel.Country = address.CountryName;
            addressViewModel.CountryCode = address.CountryCode;
            addressViewModel.FirstName = address.FirstName;
            addressViewModel.LastName = address.LastName;
            addressViewModel.Line1 = address.Line1;
            addressViewModel.Line2 = address.Line2;
            addressViewModel.Phone = address.Phone;
            addressViewModel.PostalCode = address.PostalCode;
            addressViewModel.Province = address.RegionName;

            return addressViewModel;
        }

        public static DataContracts.Cart.Address ToApiModel(this Address addressViewModel)
        {
            var address = new DataContracts.Cart.Address();

            address.City = addressViewModel.City;
            address.Organization = addressViewModel.Company;
            address.CountryName = addressViewModel.Country;
            address.CountryCode = addressViewModel.CountryCode;
            address.FirstName = addressViewModel.FirstName;
            address.LastName = addressViewModel.LastName;
            address.Line1 = addressViewModel.Line1;
            address.Line2 = addressViewModel.Line2;
            address.Phone = addressViewModel.Phone;
            address.PostalCode = addressViewModel.PostalCode;
            address.RegionName = addressViewModel.Province;

            return address;
        }
    }
}