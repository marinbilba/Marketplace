using System.Text.Json.Serialization;

namespace MarketplaceAPI.Model
{
    public class OrderDetails
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("deliveryAddress")] public string DeliveryAddress { get; set; }
        [JsonPropertyName("customerOrderId")] public int CustomerOrderId { get; set; }

        [JsonPropertyName("customerOrder")]
        [JsonIgnore]
        public CustomerOrder CustomerOrder { get; set; }
        
    }
}