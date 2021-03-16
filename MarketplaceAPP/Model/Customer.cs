using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceAPP.Model
{
    public class Customer
    {
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