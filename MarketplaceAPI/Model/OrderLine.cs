using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceAPI.Model
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}