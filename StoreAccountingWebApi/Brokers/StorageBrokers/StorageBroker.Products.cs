using StoreAccountingWebApi.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Brokers.StorageBrokers
{
    public partial class StorageBroker
    {
        DbSet<Product> Products { get; set; }

        public ValueTask<Product> InsertProductAsync(Product product) =>
            InsertAsync(product);

        public IQueryable<Product> SelectAllProducts() =>
            SelectAll<Product>();

        public ValueTask<Product> SelectProductById(int id) =>
            SelectByIdAsync<Product>(id);

        public ValueTask<Product> UpdateProductAsync(Product product) =>
            UpdateAsync(product);

        public ValueTask<Product> DeleteProductAsync(Product product) =>
            DeleteAsync(product);
    }
}
