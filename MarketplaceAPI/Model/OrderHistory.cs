using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceAPI.Model
{
    public class OrderHistory
    {
        [Key]
        public int Id { get; set; }
        public string CustomerUsername { get; set; }
        public Customer Customer { get; set; }
        public ICollection<CustomerOrder> CustomerOrder { get; set; }
    }
}