using System;

namespace StoreAccountingWebApi.Models.Products
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int id) :
            base($"product with {id} was not found")
        {
             
        }
    }
}
