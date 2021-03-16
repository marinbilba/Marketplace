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
        [JsonPropertyName("orderHistory")]

        public OrderHistory OrderHistory { get; set; }
        [JsonPropertyName("cart")]

        public Cart Cart { get; set; }
    }
}