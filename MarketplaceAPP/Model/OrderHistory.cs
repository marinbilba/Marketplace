using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceAPP.Model
{
    public class OrderHistory
    {
        [Key]
        [JsonPropertyName("id")]

        public int Id { get; set; }
        [JsonPropertyName("customerUsername")]

        public string CustomerUsername { get; set; }
        [JsonPropertyName("customer")]

        public Customer Customer { get; set; }
        [JsonPropertyName("customerOrder")]

        public ICollection<CustomerOrder> CustomerOrder { get; set; }
    }
}