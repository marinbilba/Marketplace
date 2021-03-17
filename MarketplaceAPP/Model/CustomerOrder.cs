using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceAPP.Model
{
    public class CustomerOrder
    {
        [Key]
        [JsonPropertyName("id")]

        public int Id { get; set; }
        [JsonPropertyName("cartId")]

        public int CartId { get; set; }
        [JsonIgnore]

        public int Cart { get; set; }
        [JsonPropertyName("dateTime")]

        public DateTime DateTime { get; set; }
        [JsonPropertyName("orderDetails")]
        public  OrderDetails OrderDetails { get; set; }
        
        [JsonIgnore]

        public Customer Customer{ get; set; }
        [JsonPropertyName("customerUsername")]

        public string CustomerUsername{ get; set; }
        
       
        
    }
}