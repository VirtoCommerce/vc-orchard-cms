using Orchard;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtoCommerce.Webshop.ViewModels;

namespace VirtoCommerce.Webshop.Services
{
    public interface ICatalogService : IDependency
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(string storeId, string culture);

        Task<Category> GetCategoryAsync(string storeId, string culture, string slug);

        Task<PagedList<Product>> SearchProductsAsync(string storeId, string culture, string categorySlug, int skip, int take, IEnumerable<string> pricelists);

        Task<Product> GetProductBySlugAsync(string storeId, string culture, string pricelistId, string slug);

        Task<Product> GetProductBySkuAsync(string storeId, string culture, string pricelistId, string sku);
    }
}