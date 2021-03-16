using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceAPI.Database;
using MarketplaceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceAPI.Services
{
    public class ProductService : IProductService
    {
        private MarketplaceContext dbContext { get; }

        public ProductService(MarketplaceContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IList<Product>> GetAllProductsFromCategoryAsync(Category category)
        {
            return await dbContext.Product.Where(p => p.Category.Equals(category)).ToListAsync();
        }
    }
}