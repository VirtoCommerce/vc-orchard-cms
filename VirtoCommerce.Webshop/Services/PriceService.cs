using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;
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
            var pricelistIds = await _apiClient.PriceClient.GetPriceListsAsync(catalogId, currency, new DataContracts.TagQuery()).ConfigureAwait(false);

            return pricelistIds;
        }

        public async Task<IEnumerable<Price>> GetPricesAsync(IEnumerable<string> pricelistIds, IEnumerable<string> productIds)
        {
            var apiResponse = await _apiClient.PriceClient.GetPrices(pricelistIds.ToArray(), productIds.ToArray(), false).ConfigureAwait(false);

            if (apiResponse == null)
            {
                return null;
            }

            return apiResponse.Select(p => p.ToViewModel());
        }
    }
}