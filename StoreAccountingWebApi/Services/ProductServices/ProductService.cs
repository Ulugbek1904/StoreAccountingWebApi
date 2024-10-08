using StoreAccountingWebApi.Brokers.StorageBrokers;
using StoreAccountingWebApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;
        public ProductService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;   
        }

        public async ValueTask<Product> AddProductAsync(Product product)
        {
            if (product != null)
            {
                await storageBroker.InsertProductAsync(product);

                return product;
            }
            else
                throw new NullProductException("Contact does not be null");
        }

        public IQueryable<Product> GetAllProduct()
        {
            var Products = storageBroker.SelectAllProducts();

            return Products;
        }

        public async ValueTask<Product> GetProductByIdAsync(int id)
        {
            var contact = await storageBroker.SelectProductById(id);
            if (contact == null)
                throw new ProductNotFoundException(id);

            return contact;
        }

        public async ValueTask<Product> UpdateProductAsync(Product product)
        {
            var existingProduct = await storageBroker.SelectProductById(product.Id);

            if (existingProduct == null)
            {
                throw new ProductNotFoundException(product.Id);
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Count = product.Count;
            existingProduct.ExpiryDate = product.ExpiryDate;
            existingProduct.Manufacturer = product.Manufacturer;

            await storageBroker.UpdateProductAsync(existingProduct);

            return existingProduct;
        }


        public async ValueTask<Product> DeleteProductAsync(Product product)
        {
            var existingProduct = await storageBroker.SelectProductById(product.Id);

            if (existingProduct == null)
            {
                throw new ProductNotFoundException(product.Id);
            }

            await storageBroker.DeleteProductAsync(existingProduct);

            return existingProduct;
        }

        public async ValueTask UpdateProductStockAsync(int  id, int quantitySold)
        {
            var product = await GetProductByIdAsync(id);

            if(product != null)
            {
                product.Count -= quantitySold;
                await UpdateProductAsync(product);
            }
        }

        public IQueryable<Product> GetExpiringProducts(DateTime expiryDate)
        {
            var products = storageBroker.SelectAllProducts();

            return products.Where(products => products.ExpiryDate <= expiryDate);
        }

        public async Task<bool> CheckProductAvailabilityAsync(int productId, int quantity)
        {
            var product = await storageBroker.SelectProductById(productId);

            return product != null && product.Count >= quantity;
        }
    }
}
