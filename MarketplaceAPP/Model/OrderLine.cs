using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceAPP.Model
{
    public class OrderLine
    {
        [Key]
        [JsonPropertyName("id")]

        public int Id { get; set; }
        [JsonPropertyName("quantity")]

        public int Quantity { get; set; }
        [JsonPropertyName("totalPrice")]

        public decimal TotalPrice { get; set; }
        [JsonPropertyName("cart")]

        public virtual Cart Cart { get; set; }
        [JsonPropertyName("product")]

        public virtual Product Product { get; set; }
    }
}