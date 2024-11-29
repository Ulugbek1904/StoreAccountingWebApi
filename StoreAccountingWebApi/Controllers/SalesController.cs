using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTFulSense.Controllers;
using StoreAccountingWebApi.Models.Sales;
using StoreAccountingWebApi.Services.SaleServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAccountingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SalesController : RESTFulController
    {
        private readonly ISaleService saleService;
        public SalesController(ISaleService saleService)
        {
            this.saleService = saleService;
        }

        [Authorize(Roles = "client,admin")]
        [HttpPost]
        public async ValueTask<IActionResult> BuyingProducts([FromBody] SaleRequest request)
        {
            try
            {
                var sale = await saleService.BuyingProductsAsync(request.Products,request.UserId);

                return CreatedAtAction(nameof(GetSaleById), new { id = sale.SaleId }, sale);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllSale()
        {
            IQueryable<Sale> sales = saleService.GetAllSale();

            if (await sales.CountAsync() == 0)
                 return NoContent();
            return Ok(sales);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetSaleById(int id)
        {
            var sale = await saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("{userId}")]
        public async ValueTask<IActionResult> GetSalesHistoryByUserAsync(int userId)
        {
            var sales = await saleService.GetSalesHistoryByUserAsync(userId);
            if (sales.Count == 0)
                return NoContent();
            return Ok(sales);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetProductSalesStatistics(DateTime startDate, DateTime endDate)
        {
            try
            {
                var statistics = await saleService.GetProductSalesStatistics(startDate, endDate);

                if (statistics == null || statistics.Count == 0)
                {
                    return NotFound("No sales found for the given date range.");
                }

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

    public class SaleRequest
    {
        public List<SaleProduct> Products { get; set; }
        public int UserId { get; set; }
    }
}
