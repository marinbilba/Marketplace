using System;
using System.Threading.Tasks;
using MarketplaceAPI.Database;
using MarketplaceAPI.Model;
using MarketplaceAPI.Services;
using MarketplaceAPI.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UniteTestMarketplaceAPI
{
    public class CustomerServiceTests
    {
        private CustomerService _sut;
        private MarketplaceContext _marketplaceContext;

        public CustomerServiceTests()
        {
            var options = new DbContextOptionsBuilder<MarketplaceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _marketplaceContext = new MarketplaceContext(options);
            Seed();
            _sut = new CustomerService(_marketplaceContext);
        }

        [Fact]
        public async Task LoginAsync_ShouldLoginTheUser()
        {
            // Arrange
            var customerWithValidCredentials = new Customer()
            {
                Username = "test",
                Password = "123"
            };
            // Act

            var loginCustomer = _sut.LoginAsync(customerWithValidCredentials);

            //Assert
            Assert.NotNull(loginCustomer);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowIncorrectUsernameOrPassword()
        {
            // Arrange
            bool incorrectUsername = false;
            bool incorrectPassword = false;
            var customerWithIncorrectPassword = new Customer()
            {
                Username = "test",
                Password = "12342143"
            };
            var customerWithIncorrectUsername = new Customer()
            {
                Username = "testsda",
                Password = "123"
            };
            // Act
            try
            {
                await _sut.LoginAsync(customerWithIncorrectPassword);
            }
            catch (IncorrectUsernameOrPassword e)
            {
                incorrectPassword = true;
            }

            try
            {
                await _sut.LoginAsync(customerWithIncorrectUsername);
            }
            catch (IncorrectUsernameOrPassword e)
            {
                incorrectUsername = true;
            }

            //Assert
            Assert.True(incorrectPassword);
            Assert.True(incorrectUsername);
        }

        private void Seed()
        {
            var customer = new Customer() {Username = "test", Password = "123"};
            _marketplaceContext.Add(customer);
            _marketplaceContext.SaveChanges();
        }
    }
}