﻿namespace VirtoCommerce.Webshop.Client.DataContracts
{
    public class Facet
    {
        public string FacetType { get; set; }

        public string Field { get; set; }

        public string Label { get; set; }

        public FacetValue[] Values { get; set; }
    }
}