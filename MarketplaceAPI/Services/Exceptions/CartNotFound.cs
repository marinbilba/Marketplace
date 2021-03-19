using System;

namespace MarketplaceAPI.Services.Exceptions
{
    public class CartNotFound : Exception
    {
        public CartNotFound()
        {
        }

        public CartNotFound(string message) : base(message)
        {
        }
    }
}