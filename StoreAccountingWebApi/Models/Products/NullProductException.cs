using System;

namespace StoreAccountingWebApi.Models.Products
{
    public class NullProductException : Exception
    {
        public NullProductException(string message) : base(message) { }
    }
}
