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

        public IEnumerable<string> Pricelists { get; set; }

        public string Search { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public string SortProperty { get; set; }

        public string SortDirection { get; set; }

        public DateTime? DateFrom { get; set; }

        public int? ResponseGroup { get; set; }

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

            if (Pricelists != null && Pricelists.Count() > 0)
            {
                parameters.Add("pricelists", String.Join(",", Pricelists));
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

            var stringList = new List<string>();
            foreach (var parameter in parameters)
            {
                stringList.Add(String.Format("{0}={1}", parameter.Key, parameter.Value));
            }

            return String.Join("&", stringList);
        }
    }
}