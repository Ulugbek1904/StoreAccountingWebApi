using System;

namespace StoreAccountingWebApi.Models.Sales
{
    public class NullSaleException :Exception
    {
        public NullSaleException(string message) : base(message) { }
    }
}
