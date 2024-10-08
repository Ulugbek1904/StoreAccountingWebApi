using StoreAccountingWebApi.Models.Products;
using StoreAccountingWebApi.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Services.SaleServices
{
    public interface ISaleService
    {
        ValueTask<Sale> BuyingProductsAsync(List<SaleProduct> products, int userId);
        IQueryable<Sale> GetAllSale();
        ValueTask<Sale> GetSaleByIdAsync(int SaleId);
        ValueTask<List<Sale>> GetSalesHistoryByUserAsync(int userId);
        ValueTask<List<ProductSaleStatistics>> GetProductSalesStatistics(DateTime startDate, DateTime endDate);
    }
}
