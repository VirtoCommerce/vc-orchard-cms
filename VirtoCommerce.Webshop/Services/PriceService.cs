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

            var apiResponse = await _apiClient.GetPricelistsAsync("", "USD").ConfigureAwait(false);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Body != null)
                {
                    pricelists = apiResponse.Body;
                }
            }

            return pricelists;
        }

        public async Task<IEnumerable<Price>> GetPricesAsync(IEnumerable<string> pricelists, IEnumerable<string> productIds)
        {
            var priceModels = new List<Price>();

            var apiResponse = await _apiClient.GetPricesAsync(pricelists, productIds);
            if (apiResponse != null)
            {
                if (apiResponse.Error != null)
                {
                    // TODO: Do something with errors
                    throw new Exception(apiResponse.Error.StackTrace);
                }
                if (apiResponse.Body != null)
                {
                    foreach (var price in apiResponse.Body)
                    {
                        priceModels.Add(price.ToViewModel());
                    }
                }
            }

            return priceModels;
        }
    }
}