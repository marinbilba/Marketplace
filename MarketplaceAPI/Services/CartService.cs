using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceAPI.Database;
using MarketplaceAPI.Model;
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
            Cart cart = dbContext.Cart.FirstAsync(c => c.Id == cartId).Result;
            Product product = dbContext.Product.FirstAsync(p => p.Id == productId).Result;
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
            var cart = dbContext.Cart.First(c => c.Id == order.CartId);

            order.DateTime = DateTime.Now;
            order.TotalPrice = cart.TotalPrice;
            dbContext.Entry(order).State = EntityState.Added;
            dbContext.CustomerOrder.Add(order);
            // dbContext.Customer.Update(customer);
            dbContext.SaveChanges();
            await ClearCartAsync(order.CartId);
        }

        private async Task ClearCartAsync(int cartId)
        {
            // var cart =  dbContext.Cart
            //     .FirstAsync(x => x.Id == cartId).Result;
            Cart cart = dbContext.Cart.Include(p => p.CartProduct).ThenInclude(pr=>pr.Product).FirstAsync(s => s.Id == cartId).Result;

            foreach (var cartProduct in cart.CartProduct.Where(a=>a.CartId==cart.Id))
            {
                dbContext.CartProducts.Remove(cartProduct);
                // CartProduct fetchedCartProducts = dbContext.Cart.Where(s => s.Id == cartId)
                //     .SelectMany(c => c.CartProduct).First(p => p.ProductId == cartProduct.ProductId);
                // dbContext.Remove(fetchedCartProducts);
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