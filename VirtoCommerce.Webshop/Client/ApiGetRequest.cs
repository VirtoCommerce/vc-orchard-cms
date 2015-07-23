using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace VirtoCommerce.Webshop.Client
{
    public class ApiGetRequest
    {
        public string StoreId { get; set; }

        public string Culture { get; set; }

        public IDictionary<string, string[]> Filters { get; set; }

        public string Outline { get; set; }

        public string Search { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public string SortProperty { get; set; }

        public string SortDirection { get; set; }

        public DateTime? DateFrom { get; set; }

        public int? ResponseGroup { get; set; }

        public string Keyword { get; set; }

        public string Code { get; set; }

        public string Currency { get; set; }

        public string CatalogId { get; set; }

        public IEnumerable<string> PricelistIds { get; set; }

        public IEnumerable<string> ProductIds { get; set; }

        public string CustomerId { get; set; }

        public string BuildQueryString()
        {
            var parameters = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(StoreId))
            {
                parameters.Add("store", StoreId);
            }

            if (!String.IsNullOrEmpty(Culture))
            {
                parameters.Add("language", Culture);
            }

            if (Filters != null && Filters.Count > 0)
            {
                foreach (var filter in Filters)
                {
                    parameters.Add(String.Format("t_{0}", filter.Key), String.Join(",", filter.Value));
                }
            }

            if (!String.IsNullOrEmpty(Outline))
            {
                parameters.Add("outline", Outline);
            }

            if (PricelistIds != null && PricelistIds.Count() > 0)
            {
                parameters.Add("pricelists", String.Join(",", PricelistIds));
            }

            if (!String.IsNullOrEmpty(Search))
            {
                parameters.Add("search", Search);
            }

            if (Skip.HasValue)
            {
                parameters.Add("skip", Skip.Value.ToString(CultureInfo.InvariantCulture));
            }
            
            if (Take.HasValue)
            {
                parameters.Add("take", Take.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (!String.IsNullOrEmpty(SortProperty) && !String.IsNullOrEmpty(SortDirection))
            {
                parameters.Add("sort", SortProperty);
                parameters.Add("sortorder", SortDirection);
            }

            if (DateFrom.HasValue)
            {
                parameters.Add("startdatefrom", DateFrom.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (ResponseGroup.HasValue)
            {
                parameters.Add("responseGroup", ResponseGroup.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (!String.IsNullOrEmpty(Keyword))
            {
                parameters.Add("keyword", Keyword);
            }

            if (!String.IsNullOrEmpty(Code))
            {
                parameters.Add("code", Keyword);
            }

            if (!String.IsNullOrEmpty(CatalogId))
            {
                parameters.Add("catalog", CatalogId);
            }

            if (!String.IsNullOrEmpty(Currency))
            {
                parameters.Add("currency", Currency);
            }

            if (ProductIds != null && ProductIds.Count() > 0)
            {
                parameters.Add("products", String.Join(",", ProductIds));
            }

            if (!String.IsNullOrEmpty(CustomerId))
            {
                parameters.Add("customerId", CustomerId);
            }

            var stringList = new List<string>();
            foreach (var parameter in parameters)
            {
                stringList.Add(String.Format("{0}={1}", parameter.Key, parameter.Value));
            }

            return String.Join("&", stringList);
        }
    }
}