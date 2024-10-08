using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Brokers.StorageBrokers
{
    public partial interface IStorageBroker
    {
        ValueTask<T> InsertAsync<T>(T entity) where T : class;
        IQueryable<T> SelectAll<T>() where T : class;
        ValueTask<T> SelectByIdAsync<T>(int id) where T : class;
        ValueTask<T> UpdateAsync<T>(T entity) where T : class;
        ValueTask<T> DeleteAsync<T>(T entity) where T: class;
    }
}