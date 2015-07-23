using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.Client;
using VirtoCommerce.Webshop.Converters;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public class PriceService : IPriceService
    {
        private readonly IVirtoCommerceClient _apiClient;

        public PriceService(IVirtoCommerceClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<string>> GetPricelistsAsync(string catalogId, string currency)
        {
            IEnumerable<string> pricelists = null;

            var apiRequest = new ApiGetRequest
            {
                CatalogId = catalogId,
                Currency = currency
            };

            var apiResponse = await _apiClient.GetPricelistsAsync(apiRequest).ConfigureAwait(false);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Content != null)
                {
                    pricelists = apiResponse.Content;
                }
            }

            return pricelists;
        }

        public async Task<IEnumerable<Price>> GetPricesAsync(IEnumerable<string> pricelists, IEnumerable<string> productIds)
        {
            var priceModels = new List<Price>();

            var apiRequest = new ApiGetRequest
            {
                PricelistIds = pricelists,
                ProductIds = productIds
            };

            var apiResponse = await _apiClient.GetPricesAsync(apiRequest);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Content != null)
                {
                    foreach (var price in apiResponse.Content)
                    {
                        priceModels.Add(price.ToViewModel());
                    }
                }
            }

            return priceModels;
        }
    }
}