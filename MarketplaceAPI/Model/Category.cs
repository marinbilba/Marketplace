﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketplaceAPI.Model
{
    public class Category
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]

        public string Name { get; set; }
     

    }
}