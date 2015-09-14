using Orchard;
using VirtoCommerce.ApiClient;

namespace VirtoCommerce.WebStore.ApiClient
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