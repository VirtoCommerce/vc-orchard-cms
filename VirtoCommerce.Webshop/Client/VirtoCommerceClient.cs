using Orchard;
using Orchard.ContentManagement;
using System;
using VirtoCommerce.ApiClient;
using VirtoCommerce.ApiClient.Extensions;
using VirtoCommerce.Webshop.Models;

namespace VirtoCommerce.Webshop.Client
{
    public class VirtoCommerceClient : IVirtoCommerceClient
    {
        private readonly IOrchardServices _orchardServices;
        private readonly Lazy<WebshopSettingsPart> _webshopSettings;

        public VirtoCommerceClient(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
            _webshopSettings = new Lazy<WebshopSettingsPart>(GetWebshopSettings);
        }

        public StoreClient StoreClient
        {
            get
            {
                return ClientContext.Clients.CreateStoreClient(_webshopSettings.Value.ApiUrl, _webshopSettings.Value.AppId, _webshopSettings.Value.SecretKey);
            }
        }

        public BrowseClient BrowseClient
        {
            get
            {
                return ClientContext.Clients.CreateBrowseClient(_webshopSettings.Value.ApiUrl, _webshopSettings.Value.AppId, _webshopSettings.Value.SecretKey);
            }
        }

        public PriceClient PriceClient
        {
            get
            {
                return ClientContext.Clients.CreatePriceClient(_webshopSettings.Value.ApiUrl, _webshopSettings.Value.AppId, _webshopSettings.Value.SecretKey);
            }
        }

        public CartClient CartClient
        {
            get
            {
                return ClientContext.Clients.CreateCartClient(_webshopSettings.Value.ApiUrl, _webshopSettings.Value.AppId, _webshopSettings.Value.SecretKey);
            }
        }

        public OrderClient OrderClient
        {
            get
            {
                return ClientContext.Clients.CreateOrderClient(_webshopSettings.Value.ApiUrl, _webshopSettings.Value.AppId, _webshopSettings.Value.SecretKey);
            }
        }

        private WebshopSettingsPart GetWebshopSettings()
        {
            return _orchardServices.WorkContext.CurrentSite.As<WebshopSettingsPart>();
        }
    }
}