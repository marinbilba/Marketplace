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
    public class CartService : ICartService
    {
        private MarketplaceContext dbContext { get; }

        public CartService(MarketplaceContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void DeleteProduct(int cartId, int productId)
        {
            Cart cart = dbContext.Cart.FirstOrDefaultAsync(c => c.Id == cartId).Result;
            if (cart==null)
            {
                throw new CartNotFound("Cart not found");
            }
            Product product = dbContext.Product.FirstOrDefaultAsync(p => p.Id == productId).Result;
            if (product==null)
            {
                throw new CartNotFound("Product not found");
            }
            CartProduct cartProduct = dbContext.Cart.
                Where(s => s.Id == cartId)
                .SelectMany(cp => cp.CartProduct).
                First(studentCourse => studentCourse.ProductId==productId);
  
            cart.TotalPrice -= product.Price;
            dbContext.Remove(cartProduct);
            // Update entity cart 
            dbContext.Cart.Update(cart);

            // Save changes in database
            dbContext.SaveChanges();
        }

        public async void PlaceOrder(CustomerOrder order)
        {
            var cart = await dbContext.Cart.FirstOrDefaultAsync(c => c.Id == order.CartId);
            if (cart == null)
            {
                throw new CartNotFound("Cart not found");
            }

            order.DateTime = DateTime.Now;
            order.TotalPrice = cart.TotalPrice;
            dbContext.Entry(order).State = EntityState.Added;
            dbContext.CustomerOrder.Add(order);
            dbContext.SaveChanges();
            await ClearCartAsync(order.CartId);
        }

        private async Task ClearCartAsync(int cartId)
        {
            Cart cart = dbContext.Cart.Include(p => p.CartProduct).ThenInclude(pr=>pr.Product).FirstAsync(s => s.Id == cartId).Result;

            foreach (var cartProduct in cart.CartProduct.Where(a=>a.CartId==cart.Id))
            {
                dbContext.CartProducts.Remove(cartProduct);
                try
                {
                    cart.TotalPrice -= cartProduct.Product.Price;
                    dbContext.Cart.Update(cart);
                     dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}