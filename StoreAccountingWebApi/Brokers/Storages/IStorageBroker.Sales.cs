using StoreAccountingWebApi.Models.Sales;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Brokers.StorageBrokers
{
    public partial interface IStorageBroker
    {
        ValueTask<Sale> InsertSaleAsync(Sale sale);
        IQueryable<Sale> SelectAllSales();
        ValueTask<Sale> SelectSaleByIdAsync(int id);
        ValueTask<Sale> UpdateSaleAsync(Sale sale);
        ValueTask<Sale> DeleteSaleAsync(Sale sale);
    }
}
