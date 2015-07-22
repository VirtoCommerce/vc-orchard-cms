using Orchard;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.Client;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public interface IPriceService : IDependency
    {
        Task<IEnumerable<string>> GetPricelistsAsync(string catalogId, string currency);

        Task<IEnumerable<Price>> GetPricesAsync(IEnumerable<string> pricelists, IEnumerable<string> productIds);
    }
}