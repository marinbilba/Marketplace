using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceAPI.Model;

namespace MarketplaceAPI.Services
{
    public interface ICustomerService
    {
        Task<Customer> LoginAsync(Customer user);
        Task<Cart> GetCustomerCartAsync(string customerUsername);
        
        Task<IList<CustomerOrder>> GetCustomerOrderHistoryAsync(string customerUsername);
    }

  
}