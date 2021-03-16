using System;

namespace MarketplaceAPI.Services.Exceptions
{
    public class IncorrectUsernameOrPassword : Exception
    {
        public IncorrectUsernameOrPassword()
        {
        }

        public IncorrectUsernameOrPassword(string message) : base(message) { }

    }
}