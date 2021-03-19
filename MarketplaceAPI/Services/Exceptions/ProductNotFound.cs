using System;

namespace MarketplaceAPI.Services.Exceptions
{
    public class ProductNotFound: Exception
    {
        public ProductNotFound()
        {
        }

        public ProductNotFound(string message) : base(message) { }
    
        
    }
}