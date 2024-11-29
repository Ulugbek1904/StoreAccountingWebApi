using System;

namespace StoreAccountingWebApi.Models.Users
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) :
            base(message)
        {
            
        }
    }
}
