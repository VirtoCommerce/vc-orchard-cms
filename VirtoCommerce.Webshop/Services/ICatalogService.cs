using Orchard;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public interface ICatalogService : IDependency
    {
        Task<IEnumerable<Shop>> GetShopsAsync();

        Task<IEnumerable<Category>> GetCategoriesAsync(string storeId, string culture);

        Task<Category> GetCategoryAsync(string storeId, string culture, string slug);

        Task<PagedList<Product>> SearchProductsAsync(string storeId, string culture, string categorySlug, IEnumerable<string> pricelistIds, int skip, int take);

        Task<Product> GetProductAsync(string storeId, string culture, string slug);
    }
}