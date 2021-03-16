using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MarketplaceAPP.Model;
using Microsoft.Extensions.Configuration;

namespace MarketplaceAPP.Data
{
    public class MarketplaceService : IMarketplaceService
    {
        private readonly HttpClient client;
        private readonly string uri;
        
        public MarketplaceService(IConfiguration configuration)
        {
            client = new HttpClient();
            uri = configuration.GetSection("API:Marketplace").Value;
        }
        
        public async Task<IList<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var message = await client.GetStringAsync($"{uri}/category");
                var result = JsonSerializer.Deserialize<List<Category>>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        public async Task<IList<Product>> GetAllProductsFromCategory(int categoryId)
        {
            try
            {
                var message = await client.GetStringAsync($"{uri}/{categoryId}");
                var result = JsonSerializer.Deserialize<List<Product>>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
    }
}