using Orchard;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.WebStore.ViewModels;

namespace VirtoCommerce.WebStore.Services
{
    public interface ICatalogService : IDependency
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(string storeId, string culture);

        Task<PagedList<Product>> GetProductsAsync(string storeId, string culture, string categorySlug, int skip, int take);

        Task<Product> GetProductAsync(string storeId, string culture, string slug);
    }
}