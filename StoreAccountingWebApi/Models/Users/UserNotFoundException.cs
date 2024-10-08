using System;

namespace StoreAccountingWebApi.Models.Users
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id) :
            base($"product with {id} was not found")
        {
            
        }
    }
}
