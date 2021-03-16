using System.Threading.Tasks;
using MarketplaceAPI.Model;

namespace MarketplaceAPI.Services
{
    public interface ICustomerService
    {
        Task<Customer> LoginAsync(Customer user);
    }

  
}