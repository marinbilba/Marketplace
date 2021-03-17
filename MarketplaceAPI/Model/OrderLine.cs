using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarketplaceAPI.Model
{
    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]

        public int Id { get; set; }

        [JsonPropertyName("quantity")] public int Quantity { get; set; } = 1;
        [JsonPropertyName("totalPrice")]

        public decimal TotalPrice { get; set; }
        
       [JsonIgnore]
        public  Cart Cart { get; set; }
        [JsonPropertyName("cartId")]
        public  int CartId { get; set; }
        [JsonPropertyName("product")]

        public  Product Product { get; set; }
        [JsonPropertyName("productId")]

        public  int ProductId { get; set; }
    }
}