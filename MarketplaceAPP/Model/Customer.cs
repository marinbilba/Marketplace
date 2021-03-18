using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceAPP.Model
{
    public class Customer
    {
        public Customer(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Key]
        [JsonPropertyName("username")]

        public string Username { get; set; }
        [JsonPropertyName("password")]

        public string Password { get; set; }
      
        [JsonPropertyName("cart")]

        public Cart Cart { get; set; }
        [JsonPropertyName("customerOrder")]
        public ICollection<CustomerOrder> CustomerOrder { get; set; }
    }
}