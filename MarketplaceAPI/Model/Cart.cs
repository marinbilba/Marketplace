using System.Collections.Generic;

namespace MarketplaceAPI.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerUsername { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
        public CustomerOrder CustomerOrder { get; set; }
    }
}