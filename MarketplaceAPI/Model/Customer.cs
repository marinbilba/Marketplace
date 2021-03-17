using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceAPI.Model
{
    public class Customer
    {
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