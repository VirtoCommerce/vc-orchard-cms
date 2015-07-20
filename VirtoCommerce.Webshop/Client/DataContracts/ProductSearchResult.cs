namespace VirtoCommerce.Webshop.Client.DataContracts
{
    public class ProductSearchResult : ResponseCollection<Product>
    {
        public Facet[] Facets { get; set; }
    }
}