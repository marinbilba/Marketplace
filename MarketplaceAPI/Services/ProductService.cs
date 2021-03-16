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

        public async Task<Product> AddProductToCartAsync(Product product, string customerUsername)
        {
            var cart = await dbContext.Cart.FirstAsync(x => x.CustomerUsername.Equals(customerUsername));
            var orderLine = await AddProductToOrderLine(product, cart);
            cart.OrderLines.Add(orderLine);
            // Update entity Product data sheet byte array
            dbContext.Cart.Update(cart);

            // Save changes in database
            await dbContext.SaveChangesAsync();
            return null;
        }

        private async Task<OrderLine> AddProductToOrderLine(Product product, Cart cart)
        {
            if (cart.OrderLines == null)
            {
                return await CreateOrderLine(product);
            }

            var orderLine = cart.OrderLines.First(o => o.Product.Equals(product));
            if (orderLine != null)
            {
                throw new Exception("You have this item in the cart");
            }

            return await CreateOrderLine(product);
        }

        private async Task<OrderLine> CreateOrderLine(Product product)
        {
            
            var createOrderLine = new OrderLine()
            {Id =dbContext.OrderLine.LastAsync().Id+1,
                Product = product
            };
            await dbContext.OrderLine.AddAsync(createOrderLine);

            dbContext.Entry(createOrderLine).State = EntityState.Added;
            await dbContext.SaveChangesAsync();
            return createOrderLine;
        }
    }
}