using Microsoft.EntityFrameworkCore;
using StoreAccountingWebApi.Brokers.StorageBrokers;
using StoreAccountingWebApi.Models.Products;
using StoreAccountingWebApi.Models.Sales;
using StoreAccountingWebApi.Models.Users;
using StoreAccountingWebApi.Services.ProductServices;
using StoreAccountingWebApi.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Services.SaleServices
{
    public class SaleService : ISaleService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IProductService productService;
        private readonly IUserService userService;
        public SaleService(IStorageBroker storageBroker, IProductService productService, IUserService userService)
        {
            this.storageBroker = storageBroker;
            this.productService = productService;
            this.userService = userService;
        }

        public async ValueTask<Sale> BuyingProductsAsync(List<SaleProduct> products, int userId)
        {
            var user = await userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            foreach (var product in products)
            {
                bool isAvailable = await productService.
                    CheckProductAvailabilityAsync(product.ProductId, product.Quantity);

                if (!isAvailable)
                {
                    throw new Exception($"Product with ID {product.ProductId} " +
                        $"is not available in the requested quantity");
                }
            }

            var sale = new Sale
            {
                UserId = userId,
                SaleDate = DateTime.Now,
                SaleProducts = products,
                TotalAmount = products.Sum(saleProduct => saleProduct.Quantity * saleProduct.PriceAtSale),
            };

            await storageBroker.InsertSaleAsync(sale);

            foreach (var product in products)
            {
                await productService.
                    UpdateProductStockAsync(product.ProductId, product.Quantity);
            }

            return sale;
        }

        public IQueryable<Sale> GetAllSale()
        {
            var Sales = storageBroker.SelectAllSales();

            return Sales;
        }

        public async ValueTask<Sale> GetSaleByIdAsync(int SaleId)
        {
            var sale = await storageBroker.SelectSaleByIdAsync(SaleId);
            if (sale == null)
                throw new SaleNotFoundException(SaleId);

            return sale;
        }

        public async ValueTask<List<Sale>> GetSalesHistoryByUserAsync(int userId)
        {
            var Sales = storageBroker.SelectAllSales();

            return await Sales.Where(sale => sale.UserId == userId).
                Include(sale => sale.SaleProducts).
                ThenInclude(saleProduct => saleProduct.Product).ToListAsync();
        }

        public async ValueTask<List<ProductSaleStatistics>> GetProductSalesStatistics(DateTime startDate, DateTime endDate)
        {
            var sales = storageBroker.SelectAllSales();

            return await sales.Where(sale => sale.SaleDate >= startDate && sale.SaleDate <= endDate).
                SelectMany(sale => sale.SaleProducts)
                .GroupBy(saleProduct => new {saleProduct.ProductId, saleProduct.Product.Name})
                .Select(group => new ProductSaleStatistics
                {
                    ProductId = group.Key.ProductId,
                    ProductName = group.Key.Name,
                    TotalSold = group.Sum(sp => sp.Quantity)
                }).ToListAsync();
        }

    }
}
