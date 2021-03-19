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
    public class CategoryServiceTests
    {
        private CategoryService _sut;
        private MarketplaceContext _marketplaceContext;

        public CategoryServiceTests()
        {
            var options = new DbContextOptionsBuilder<MarketplaceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _marketplaceContext = new MarketplaceContext(options);
            Seed(_marketplaceContext);
            _sut = new CategoryService(_marketplaceContext);
        }

        [Fact]
        public async Task GetAllCategoriesAsync()
        {
            // Act
            await _sut.GetAllCategoriesAsync();

            // Assert
            Assert.Equal(2, _marketplaceContext.Category.Count());
        }

        private void Seed(MarketplaceContext marketplaceContext)
        {
            var category = new Category() {Id = 1, Name = "Computers"};
            var category1 = new Category() {Id = 2, Name = "Toys"};
            _marketplaceContext.Category.Add(category);
            marketplaceContext.Category.Add(category1);
            _marketplaceContext.SaveChanges();
        }
    }
}