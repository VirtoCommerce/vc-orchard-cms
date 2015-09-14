using Orchard;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Services
{
    public interface IPriceService : IDependency
    {
        Task<IEnumerable<Price>> GetPricesAsync(string catalogId, string currency, IEnumerable<string> productIds);
    }
}