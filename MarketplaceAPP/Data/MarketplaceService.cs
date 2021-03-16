using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
                var message = await client.GetStringAsync($"{uri}/product/{categoryId}");
                var result = JsonSerializer.Deserialize<List<Product>>(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        public async Task<Product> AddProductToCartAsync(Product product, string currentUserUsername)
        {
            Product productDeserialize = null;
            HttpResponseMessage responseMessage;
            string userSerialized = JsonSerializer.Serialize(product);
            var content = new StringContent(userSerialized, Encoding.UTF8, "application/json");
            // 1. Send POST request
            try
            {
                responseMessage =
                    await client.PostAsync($"{uri}/product/product_to_cart/{currentUserUsername}", content);
                // 2. Check if the resource was found, else throw exception to the client
                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Ooops, resource not found");
                }
            }
            // 3. Catch the exception in case the Server is not running 
            catch (HttpRequestException e)
            {
                throw new Exception("No connection could be made because the server is not responding");
            }
          
            if (responseMessage.IsSuccessStatusCode)
            {
                // 5. Deserialize the object
                string readAsStringAsync = await responseMessage.Content.ReadAsStringAsync();
                productDeserialize = JsonSerializer.Deserialize<Product>(readAsStringAsync);
                Console.WriteLine(readAsStringAsync);
            }

            return productDeserialize;
        }

        public async Task<Customer> LoginUser(Customer customer)
        {
            Customer userDeserialize = null;
            HttpResponseMessage responseMessage;
            string userSerialized = JsonSerializer.Serialize(customer);
            var content = new StringContent(userSerialized, Encoding.UTF8, "application/json");
            // 1. Send POST request
            try
            {
                responseMessage =
                    await client.PostAsync(uri + "/customer/login", content);
                // 2. Check if the resource was found, else throw exception to the client
                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Ooops, resource not found");
                }
            }
            // 3. Catch the exception in case the Server is not running 
            catch (HttpRequestException e)
            {
                throw new Exception("No connection could be made because the server is not responding");
            }
            
            string serverMessage = responseMessage.Content.ReadAsStringAsync().Result;
            // 4. Check the response status codes, else throws the error message to the client
            if (responseMessage.IsSuccessStatusCode)
            {
                // 5. Deserialize the object
                string readAsStringAsync = await responseMessage.Content.ReadAsStringAsync();
                userDeserialize = JsonSerializer.Deserialize<Customer>(readAsStringAsync);
                Console.WriteLine(readAsStringAsync);
            }
            
            else if (responseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                Console.WriteLine();
            
                throw new Exception(serverMessage);
            }
            else if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new Exception(serverMessage);
            }

            return userDeserialize;

        }
    }
}