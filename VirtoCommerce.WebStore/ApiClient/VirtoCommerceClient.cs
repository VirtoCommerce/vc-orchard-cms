using Orchard;
using Orchard.ContentManagement;
using VirtoCommerce.ApiClient;
using VirtoCommerce.ApiClient.Extensions;
using VirtoCommerce.WebStore.Models;

namespace VirtoCommerce.WebStore.ApiClient
{
    public class VirtoCommerceClient : IVirtoCommerceClient
    {
        private readonly string _apiUrl;
        private readonly string _applicationId;
        private readonly string _secretKey;

        public VirtoCommerceClient(IOrchardServices orchardServices)
        {
            var settings = orchardServices.WorkContext.CurrentSite.As<WebStoreSettingsPart>();

            _apiUrl = settings.ApiUrl;
            _applicationId = settings.ApplicationId;
            _secretKey = settings.SecretKey;
        }

        public VirtoCommerceClient(string apiUrl, string applicationId, string secretKey)
        {
            _apiUrl = apiUrl;
            _applicationId = applicationId;
            _secretKey = secretKey;
        }

        public BrowseClient BrowseClient
        {
            get
            {
                return ClientContext.Clients.CreateBrowseClient(_apiUrl, _applicationId, _secretKey);
            }
        }

        public CartClient CartClient
        {
            get
            {
                return ClientContext.Clients.CreateCartClient(_apiUrl, _applicationId, _secretKey);
            }
        }

        public OrderClient OrderClient
        {
            get
            {
                return ClientContext.Clients.CreateOrderClient(_apiUrl, _applicationId, _secretKey);
            }
        }

        public PriceClient PriceClient
        {
            get
            {
                return ClientContext.Clients.CreatePriceClient(_apiUrl, _applicationId, _secretKey);
            }
        }

        public StoreClient StoreClient
        {
            get
            {
                return ClientContext.Clients.CreateStoreClient(_apiUrl, _applicationId, _secretKey);
            }
        }
    }
}