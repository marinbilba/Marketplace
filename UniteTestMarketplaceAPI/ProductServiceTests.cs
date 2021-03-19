using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketplaceAPI.Database;
using MarketplaceAPI.Model;
using MarketplaceAPI.Services;
using MarketplaceAPI.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UniteTestMarketplaceAPI
{
    public class ProductServiceTests
    {
        private ProductService _sut;
        private MarketplaceContext _marketplaceContext;

        public ProductServiceTests()
        {
            var options = new DbContextOptionsBuilder<MarketplaceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _marketplaceContext = new MarketplaceContext(options);
            Seed();
            _sut = new ProductService(_marketplaceContext);
        }

        [Fact]
        public async Task GetAllProductsFromCategoryAsync()
        {
            // Arrange

            // Act

            var productsList = await _sut.GetAllProductsFromCategoryAsync(1);

            //Assert
            Assert.NotEmpty(productsList);
            Assert.Equal(2, productsList.Count);
        }

        [Fact]
        public async Task AddProductToCartAsync_ShouldAddExistingProductToExistingCart()
        {
            // Arrange
            var fetchedProduct = await _marketplaceContext.Product.FirstAsync(x => x.Id == 1);

            // Act
            await _sut.AddProductToCartAsync(fetchedProduct, "test");

            //Assert
            Assert.Equal(1, _marketplaceContext.CartProducts.Count());
        }

        [Fact]
        public async Task AddProductToCartAsync_AddNotExistingProductToExistingCart()
        {
            // Arrange
            bool callFailed = false;
            var product = new Product()
            {
                Id = 5,
                Name = "ACER G502",
                Description = "Lorem",
                Price = 350,
                ThumbnailUrl =
                    "https://res.cloudinary.com/dxfq3iotg/image/upload/v1571750967/Ecommerce/ef192a21ec96.jpg",
                Stock = 19,
                CategoryId = 1
            };

            // Act
            try
            {
                await _sut.AddProductToCartAsync(product, "test");
            }
            catch (ProductNotFound e)
            {
                callFailed = true;
            }
            //Assert
            Assert.True(callFailed);
        }
        [Fact]
        public async Task AddProductToCartAsync_AddNotExistingProductToNotExistingCart()
        {
            // Arrange
            bool callFailed = false;
            var product = new Product()
            {
                Id = 5,
                Name = "ACER G502",
                Description = "Lorem",
                Price = 350,
                ThumbnailUrl =
                    "https://res.cloudinary.com/dxfq3iotg/image/upload/v1571750967/Ecommerce/ef192a21ec96.jpg",
                Stock = 19,
                CategoryId = 1
            };

            // Act
            try
            {
                await _sut.AddProductToCartAsync(product, "testsa");
            }
            catch (ProductNotFound e)
            {
                callFailed = true;
            } catch (CartNotFound e)
            {
                callFailed = true;
            }
            //Assert
            Assert.True(callFailed);
        }
        [Fact]
        public async Task AddProductToCartAsync_AddExistingProductToNotExistingCart()
        {
            // Arrange
            bool callFailed = false;
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
            
            // Act
            try
            {
                await _sut.AddProductToCartAsync(product, "testhbb");
            }
             catch (CartNotFound e)
            {
                callFailed = true;
            }
            //Assert
            Assert.True(callFailed);
        }


        private void Seed()
        {
            var customer = new Customer() {Username = "test", Password = "123"};
            var cart = new Cart() {Id = 1, CustomerUsername = "test"};
            var category = new Category() {Id = 1, Name = "Computers"};
            var category1 = new Category() {Id = 2, Name = "Toys"};
            _marketplaceContext.Category.Add(category);
            _marketplaceContext.Category.Add(category1);
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
            _marketplaceContext.Customer.Add(customer);
            _marketplaceContext.Cart.Add(cart);
            _marketplaceContext.Product.Add(product);
            _marketplaceContext.Product.Add(product2);
            _marketplaceContext.SaveChanges();
        }
    }
}