using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceAPI.Model
{
    public class CustomerOrder
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public DateTime DateTime { get; set; }
        public Cart Cart { get; set; }
        public  OrderDetails OrderDetails { get; set; }
        public  Customer Customer{ get; set; }
        
       
        
    }
}