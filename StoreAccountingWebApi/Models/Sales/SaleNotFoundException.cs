using System;
namespace StoreAccountingWebApi.Models.Sales
{
    public class SaleNotFoundException : Exception
    {
        public SaleNotFoundException(int id) :
            base($"Sale with {id} was not found")
        {
            
        }
    }
}
