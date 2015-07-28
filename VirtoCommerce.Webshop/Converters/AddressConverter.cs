using DataContracts = VirtoCommerce.ApiClient.DataContracts;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Converters
{
    public static class AddressConverter
    {
        public static Address ToViewModel(this DataContracts.Cart.Address address)
        {
            var addressModel = new Address();

            addressModel.AddressLine1 = address.Line1;
            addressModel.AddressLine2 = address.Line2;
            addressModel.City = address.City;
            addressModel.Company = address.Organization;
            addressModel.Country = address.CountryName;
            addressModel.CountryCode = address.CountryCode;
            addressModel.Email = address.Email;
            addressModel.FirstName = address.FirstName;
            addressModel.LastName = address.LastName;
            addressModel.Phone = address.Phone;
            addressModel.PostalCode = address.PostalCode;
            addressModel.Province = address.RegionName;

            return addressModel;
        }

        public static Address ToViewModel(this DataContracts.Orders.Address address)
        {
            var addressModel = new Address();

            addressModel.AddressLine1 = address.Line1;
            addressModel.AddressLine2 = address.Line2;
            addressModel.City = address.City;
            addressModel.Company = address.Organization;
            addressModel.Country = address.CountryName;
            addressModel.CountryCode = address.CountryCode;
            addressModel.Email = address.Email;
            addressModel.FirstName = address.FirstName;
            addressModel.LastName = address.LastName;
            addressModel.Phone = address.Phone;
            addressModel.PostalCode = address.PostalCode;
            addressModel.Province = address.RegionName;

            return addressModel;
        }

        public static DataContracts.Cart.Address ToApiModel(this Address address)
        {
            var addressModel = new DataContracts.Cart.Address();

            addressModel.Line1 = address.AddressLine1;
            addressModel.Line2 = address.AddressLine2;
            addressModel.City = address.City;
            addressModel.Organization = address.Company;
            addressModel.CountryName = address.Country;
            addressModel.CountryCode = "USA";
            addressModel.Email = address.Email;
            addressModel.FirstName = address.FirstName;
            addressModel.LastName = address.LastName;
            addressModel.Phone = address.Phone;
            addressModel.PostalCode = address.PostalCode;
            addressModel.RegionName = address.Province;

            return addressModel;
        }
    }
}