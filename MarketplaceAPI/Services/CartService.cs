using System;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceAPI.Database;
using MarketplaceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceAPI.Services
{
    public class CartService :ICartService
    {
        private MarketplaceContext dbContext { get; }

        public CartService(MarketplaceContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void DeleteProduct(int cartId,int productId)
        {
            Cart cart=dbContext.Cart.Include(p=>p.Products).FirstAsync(c => c.Id == cartId).Result;
            if (cart == null)
            {
                throw new Exception("Cart not found");
            }
            var product = cart.Products.First(p => p.Id == productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            cart.TotalPrice -= product.Price;
            cart.Products.Remove(product);
            
            // Update entity cart 
            dbContext.Cart.Update(cart);

            // Save changes in database
            dbContext.SaveChanges();
        }

        public async void PlaceOrder(CustomerOrder order)
        {
            var customer = dbContext.Customer.Include(c=>c.CustomerOrder).FirstAsync(c=>c.Username.Equals(order.CustomerUsername)).Result;
           customer.CustomerOrder.Add(order);
            
           dbContext.Customer.Update(customer);
            dbContext.SaveChanges();
            await ClearCartAsync(order.CartId);
        }

        private async Task ClearCartAsync(int cartId)
        {
            var cart = await dbContext.Cart
                .SingleOrDefaultAsync(x => x.Id == cartId);
            cart.Products = null;
            
            cart.TotalPrice = 0;
            dbContext.Cart.Update(cart);
            dbContext.SaveChanges();
        }
    }
}