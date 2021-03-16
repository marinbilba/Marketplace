using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceAPI.Model;

namespace MarketplaceAPI.Services
{
    public interface ICategoryService
    {
        Task<IList<Category>> GetAllCategoriesAsync();
    }
}