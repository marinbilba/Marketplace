using System;

namespace MarketplaceAPI.Services.Exceptions
{
    public class ProductAlreadyInCart: Exception
    {
        public ProductAlreadyInCart()
        {
        }

        public ProductAlreadyInCart(string message) : base(message) { }

    
        
    }
}