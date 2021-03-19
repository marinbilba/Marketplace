using System;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceAPI.Database;
using MarketplaceAPI.Model;
using MarketplaceAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UniteTestMarketplaceAPI
{
    public class CartServiceTests
    {
        private CartService _sut;
        private MarketplaceContext _marketplaceContext;

        public CartServiceTests()
        {
            var options = new DbContextOptionsBuilder<MarketplaceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _marketplaceContext = new MarketplaceContext(options);
            Seed(_marketplaceContext);
            _sut = new CartService(_marketplaceContext);
        }

        [Fact]
        [Trait("Category", "DeleteProduct")]
        public async Task DeleteProductAsync_ShouldDeleteProductFromCart()
        {
            // Arrange
            await SeedCartWithOneProduct();

            // Act
            _sut.DeleteProduct(1, 1);

            // Assert
            Assert.Equal(0, _marketplaceContext.CartProducts.Count());
        }

        [Fact]
        public async Task DeleteProductAsync_DeleteProductFromNotExistingCart()
        {
            // Arrange
            await SeedCartWithOneProduct();
            bool callFailed = false;
            // Act
            try
            {
                _sut.DeleteProduct(15, 1);
            }
            catch (Exception e)
            {
                callFailed = true;
            }


            // Assert
            Assert.True(callFailed);
        }

        [Fact]
        public async Task PlaceOrderAsync_ShouldAddANewRecordInCustomerOrder()
        {
            // Arrange
            await SeedCartWithOneProduct();
            var customerOrder = new CustomerOrder()
            {
                CartId = 1,
                CustomerUsername = "test"
            };
            // Act
            _sut.PlaceOrder(customerOrder);

            // Assert
            Assert.Equal(1, _marketplaceContext.CustomerOrder.Count());
        }

        [Fact] public async Task PlaceOrderAsync_ShouldClearTheCart()
        {
            // Arrange
            await SeedCartWithOneProduct();
            var customerOrder = new CustomerOrder()
            {
                CartId = 1,
                CustomerUsername = "test"
            };
             _sut.PlaceOrder(customerOrder);
            // Act

            var productCart = _marketplaceContext.CartProducts.Where(c => c.CartId == 1).ToList();
     
            // Assert
            Assert.Empty(productCart);
        }

        private async Task SeedCartWithOneProduct()
        {
            var cart = await _marketplaceContext.Cart.FirstAsync(x => x.CustomerUsername.Equals("test"));
            var fetchedProduct = await _marketplaceContext.Product.FirstAsync(x => x.Id == 1);
            CartProduct cp = new CartProduct()
            {
                Cart = cart,
                Product = fetchedProduct
            };

            _marketplaceContext.CartProducts.Add(cp);
            _marketplaceContext.SaveChanges();
        }

        private void Seed(MarketplaceContext context)
        {
            var customer = new Customer() {Username = "test", Password = "123"};
            var cart = new Cart() {Id = 1, CustomerUsername = "test"};
            var product = new Product()
            {
                Id = 1,
                Name = "ACER G502",
                Description = "Lorem",
                Price = 350,
                ThumbnailUrl =
                    "https://res.cloudinary.com/dxfq3iotg/image/upload/v1571750967/Ecommerce/ef192a21ec96.jpg",
                Stock = 19,
                CategoryId = 1
            };
            var product2 = new Product()
            {
                Id = 2, Name = "ACER Predator", Description = "Lorem", Price = 1350,
                ThumbnailUrl = "https://www.komplett.dk/img/p/1200/1168528.jpg", Stock = 5, CategoryId = 1
            };
            context.Customer.Add(customer);
            context.Cart.Add(cart);
            context.Product.Add(product);
            context.Product.Add(product2);
            context.SaveChanges();
        }
    }
}