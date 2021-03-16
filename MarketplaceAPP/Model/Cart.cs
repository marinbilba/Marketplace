using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketplaceAPP.Model
{
    public class Cart
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }
        [JsonPropertyName("customerUsername")]
        public string CustomerUsername { get; set; }
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }
        [JsonPropertyName("orderLines")]
        public ICollection<OrderLine> OrderLines { get; set; }
        [JsonPropertyName("customerOrder")]
        public CustomerOrder CustomerOrder { get; set; }
    }
}