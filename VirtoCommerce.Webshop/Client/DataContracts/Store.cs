using System.Collections.Generic;

namespace VirtoCommerce.Webshop.Client.DataContracts
{
    public class Store
    {
        public string Catalog { get; set; }

        public string Country { get; set; }

        public string[] Currencies { get; set; }

        public string DefaultCurrency { get; set; }

        public string DefaultLanguage { get; set; }

        public string Description { get; set; }

        public string Id { get; set; }

        public string[] Languages { get; set; }

        public string[] LinkedStores { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        public string SecureUrl { get; set; }

        public SeoKeyword[] Seo { get; set; }

        public IDictionary<string, object> Settings { get; set; }

        public int StoreState { get; set; }

        public string TimeZone { get; set; }

        public string Url { get; set; }

        public string State { get; set; }

        public PaymentMethod[] PaymentMethods { get; set; }
    }
}