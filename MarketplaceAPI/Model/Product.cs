using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MarketplaceAPI.Model
{
    public class Product
    {
        [Key]
        [JsonPropertyName("id")]

        public int Id { get; set; } 
            [JsonPropertyName("name")]

        public string Name { get; set; }
        [JsonPropertyName("Description")]

        public string Description { get; set; }
        [JsonPropertyName("price")]

        public decimal Price { get; set; }
        [JsonPropertyName("thumbnailUrl")]

        public string ThumbnailUrl { get; set; }
        [JsonPropertyName("stock")]

        public int Stock { get; set; }

        [JsonIgnore]
       public Category Category { get; set; } 
       [JsonPropertyName("categoryId")]
       public int CategoryId { get; set; } 
       
        
        // public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}