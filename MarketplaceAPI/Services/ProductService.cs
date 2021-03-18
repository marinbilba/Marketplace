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
            var cart = await dbContext.Cart.FirstAsync(x => x.CustomerUsername.Equals(customerUsername));
            var fetchedProduct = await dbContext.Product.FirstAsync(x => x.Id==product.Id);

            //CheckIfProductAddedToTheCard(cart, product);
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
        
    }
}