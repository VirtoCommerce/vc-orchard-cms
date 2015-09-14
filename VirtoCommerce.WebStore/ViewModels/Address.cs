using System.ComponentModel.DataAnnotations;

namespace VirtoCommerce.WebStore.ViewModels
{
    public class Address
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Company { get; set; }

        [Required]
        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string Province { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        [Required]
        public string PostalCode { get; set; }

        public string Phone { get; set; }
    }
}