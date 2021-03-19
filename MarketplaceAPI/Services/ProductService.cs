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
            var cart = await dbContext.Cart.FirstOrDefaultAsync(x => x.CustomerUsername.Equals(customerUsername));
            if (cart == null)
            {
                throw new CartNotFound("Cart not found");
            }
            var fetchedProduct = await dbContext.Product.FirstOrDefaultAsync(x => x.Id==product.Id);
            if (fetchedProduct == null)
            {
                throw new ProductNotFound("Product not found");
            }

            await CheckIfProductAddedToTheCard(cart, fetchedProduct);
            // ensure that only one entity instance with a given key value is attached.
            
         //   dbContext.Entry(cart).State = EntityState.Detached;
     //       cart.Products.Add(product);
            cart.TotalPrice += product.Price;
            CartProduct cp = new CartProduct()
            {
                Cart = cart,
                Product = fetchedProduct
            };
          
            
            // Update entity cart with new total price
            dbContext.Set<CartProduct>().Add(cp);

            // Save changes in database
            dbContext.SaveChanges();

        }

        private async Task CheckIfProductAddedToTheCard(Cart cart, Product product)
        {
            List<Product> products = await dbContext.Cart
                .Where(s => s.Id == cart.Id)
                .SelectMany(st => st.CartProduct)
                .Select(pr => pr.Product)
                .ToListAsync();
            foreach (var productFromCart in products)
            {
                if (productFromCart.Id == product.Id)
                {
                    throw new ProductAlreadyInCart("Product is already in your cart");
                }
            }

        }
    }
}