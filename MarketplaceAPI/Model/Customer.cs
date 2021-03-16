using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceAPI.Model
{
    public class Customer
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public OrderHistory OrderHistory { get; set; }
        public Cart Cart { get; set; }
    }
}