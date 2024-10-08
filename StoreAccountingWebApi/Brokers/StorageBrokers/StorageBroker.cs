using StoreAccountingWebApi.Models.Products;
using StoreAccountingWebApi.Models.Sales;
using StoreAccountingWebApi.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STX.EFxceptions.SqlServer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Brokers.StorageBrokers
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private IConfiguration configuration;
        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        public async ValueTask<T> InsertAsync<T>(T entity) where T : class
        {
             await this.Set<T>().AddAsync(entity);
             await this.SaveChangesAsync();

            return entity;
        }

        public  IQueryable<T> SelectAll<T>() where T : class
        {
            return this.Set<T>();
        }

        public async ValueTask<T> SelectByIdAsync<T>(int id) where T : class
        {
            return await this.Set<T>().FindAsync(id);
        }

        public async ValueTask<T> UpdateAsync<T>(T entity) where T : class
        {
            this.Set<T>().Update(entity);
            await this.SaveChangesAsync();

            return entity;
        }

        public async ValueTask<T> DeleteAsync<T>(T entity) where T : class
        {
            this.Set<T>().Remove(entity);
            await this.SaveChangesAsync();

            return entity;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration.GetConnectionString("DefaultConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
