using StoreAccountingWebApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Services.ProductServices
{
    public interface IProductService
    {
        ValueTask<Product> AddProductAsync(Product product);
        IQueryable<Product> GetAllProduct();
        ValueTask<Product> GetProductByIdAsync(int id);
        ValueTask<Product> UpdateProductAsync(Product product);
        ValueTask<Product> DeleteProductAsync(Product product);
        ValueTask UpdateProductStockAsync(int id, int quantity);
        IQueryable<Product> GetExpiringProducts(DateTime expiryDate);
        Task<bool> CheckProductAvailabilityAsync(int productId, int quantity);
    }
}