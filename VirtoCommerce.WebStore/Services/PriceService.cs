using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ApiClient;
using VirtoCommerce.WebStore.Converters;
using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Services
{
    public class PriceService : IPriceService
    {
        private readonly IVirtoCommerceClient _apiClient;

        public PriceService(IVirtoCommerceClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Price>> GetPricesAsync(string catalogId, string currency, IEnumerable<string> productIds)
        {
            var pricelistIds = await _apiClient.PriceClient.GetPriceListsAsync(catalogId, currency, new DataContracts.TagQuery()).ConfigureAwait(false);

            if (pricelistIds == null)
            {
                return null;
            }

            var prices = await _apiClient.PriceClient.GetPrices(pricelistIds, productIds.ToArray()).ConfigureAwait(false);

            if (prices == null)
            {
                return null;
            }

            var priceViewModels = prices.Select(p => p.ToViewModel());

            return priceViewModels;
        }
    }
}