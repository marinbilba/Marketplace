using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceAPI.Database;
using MarketplaceAPI.Model;
using MarketplaceAPI.Services.Exceptions;

namespace MarketplaceAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private MarketplaceContext dbContext { get; }

        public CustomerService(MarketplaceContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Customer> LoginAsync(Customer user)
        {
            var fetchedUser =  dbContext.Customer.FirstOrDefault(c => c.Username.Equals(user.Username));
            if (fetchedUser==null||fetchedUser.Password != user.Password)
            {
                throw new IncorrectUsernameOrPassword("Password or username are incorrect");
            }

            return fetchedUser;
        }
    }
}