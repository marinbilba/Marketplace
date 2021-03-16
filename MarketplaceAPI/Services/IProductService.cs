using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceAPI.Model;

namespace MarketplaceAPI.Services
{
    public interface IProductService
    {
        Task<IList<Product>> GetAllProductsFromCategoryAsync(int categoryId);

        Task<Product> AddProductToCartAsync(Product product, string customerUsername);
    }
}