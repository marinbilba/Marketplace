using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceAPI.Model
{
    public class CustomerOrder
    {
        [Key]
        [JsonPropertyName("id")]

        public int Id { get; set; }
        [JsonPropertyName("cartId")]

        public int CartId { get; set; }
        [JsonIgnore]

        public Cart Cart { get; set; }
        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
        [JsonPropertyName("numberOfProducts")]
        public int NumberOfProducts { get; set; }
        
        [JsonPropertyName("dateTime")]

        public DateTime DateTime { get; set; }
        [JsonIgnore]

        public Customer Customer{ get; set; }
        [JsonPropertyName("customerUsername")]

        public string CustomerUsername{ get; set; }

        
       
        
    }
}