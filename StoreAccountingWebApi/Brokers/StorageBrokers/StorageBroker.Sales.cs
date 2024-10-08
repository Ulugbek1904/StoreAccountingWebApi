using StoreAccountingWebApi.Models.Sales;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Brokers.StorageBrokers
{
    public partial class StorageBroker
    {
        DbSet<Sale> Sales { get; set; }

        public ValueTask<Sale> InsertSaleAsync(Sale sale) =>
            InsertAsync(sale);

        public IQueryable<Sale> SelectAllSales() =>
            SelectAll<Sale>();

        public ValueTask<Sale> SelectSaleByIdAsync(int id) =>
            SelectByIdAsync<Sale>(id);

        public ValueTask<Sale> UpdateSaleAsync(Sale sale) =>
            UpdateAsync(sale);
        public ValueTask<Sale> DeleteSaleAsync(Sale sale) =>
            DeleteAsync(sale);
    }
}
