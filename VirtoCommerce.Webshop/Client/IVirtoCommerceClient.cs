using Orchard;
using VirtoCommerce.ApiClient;

namespace VirtoCommerce.Webshop.Client
{
    public interface IVirtoCommerceClient : IDependency
    {
        StoreClient StoreClient { get; }

        BrowseClient BrowseClient { get; }

        PriceClient PriceClient { get; }

        CartClient CartClient { get; }

        OrderClient OrderClient { get; }
    }
}