using System;
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

        public async Task<IList<Product>> GetAllProductsFromCategoryAsync(int categoryId)
        {
            return await dbContext.Product.Where(p => p.Category.Id == categoryId).ToListAsync();
        }

        public async Task AddProductToCartAsync(Product product, string customerUsername)
        {
            var cart = await dbContext.Cart.Include(c=>c.OrderLines).FirstAsync(x => x.CustomerUsername.Equals(customerUsername));
            await CreateOrderLine(cart,product);
            cart.TotalPrice += product.Price;
            
            // Update entity cart with new total price
            dbContext.Cart.Update(cart);

            // Save changes in database
            dbContext.SaveChanges();

        }

        private async Task AddProductToOrderLine(Product product, Cart cart)
        {
            if (cart.OrderLines.Count==0)
            {
                 await CreateOrderLine(cart,product);
            }
            else
            {
                var orderLine = cart.OrderLines.Where(o=>o.Id==product.Id).ToList();
                if (orderLine.Count != 0)
                {
                    throw new Exception("You have this item in the cart");
                }

               await CreateOrderLine(cart, product);
            }

        }

        private async Task CreateOrderLine(Cart cart,Product product)
        {

            var createOrderLine = new OrderLine()
            {
                Id = dbContext.OrderLine.ToList().Count+1,
                CartId = cart.Id,
                ProductId = product.Id
                
                
            };
            dbContext.OrderLine.Add(createOrderLine);

            dbContext.Entry(createOrderLine).State = EntityState.Added;
             dbContext.SaveChanges();
           
        }
    }
}