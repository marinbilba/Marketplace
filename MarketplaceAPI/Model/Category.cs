using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceAPI.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}