using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceAPI.Database;
using MarketplaceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private MarketplaceContext dbContext { get; }

        public CategoryService(MarketplaceContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.Category.ToListAsync();
        }
        
    }
}