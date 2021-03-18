using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceAPI.Database;
using MarketplaceAPI.Model;
using MarketplaceAPI.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Cart> GetCustomerCartAsync(string customerUsername)
        {
            var cart = dbContext.Cart.FirstOrDefault(c => c.CustomerUsername.Equals(customerUsername));
            if (cart != null)
            {
                List<Product> products = await dbContext.Cart
                    .Where(s => s.CustomerUsername.Equals(customerUsername))
                    .SelectMany(student => student.CartProduct)
                    .Select(studentCourse => studentCourse.Product)
                    .ToListAsync();

                cart.Products = products;
                return cart;
            }

            throw new Exception("Cart not found");
        }

        public async Task<IList<CustomerOrder>> GetCustomerOrderHistoryAsync(string customerUsername)
        {
            return dbContext.CustomerOrder.Where(c=>c.CustomerUsername.Equals(customerUsername)).ToList();
           
        }
    }
}