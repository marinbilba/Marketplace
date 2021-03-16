using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceAPP.Model;

namespace MarketplaceAPP.Data
{
    public interface IMarketplaceService
    {
        Task<IList<Category>> GetAllCategoriesAsync();
        Task<IList<Product>> GetAllProductsFromCategory(int categoryId);
    }
}