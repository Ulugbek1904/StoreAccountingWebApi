using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreAccountingWebApi.Models.Products;
using StoreAccountingWebApi.Services.ProductServices;
using RESTFulSense.Controllers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreAccountingWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : RESTFulController
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;            
        }

        [HttpPost]
        public async ValueTask<ActionResult<Product>> CreateProduct(Product product)
        {
            var newProduct = await productService.AddProductAsync(product);

            return newProduct;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsStock()
        {
            IQueryable<Product> products = productService.GetAllProduct();

            if (await products.CountAsync() == 0)
                return NoContent();

            return Ok(await products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await productService.GetProductByIdAsync(id);

                return Ok(product);
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetExpiryProducts()
        {
            IQueryable<Product> expiredProducts = productService
                .GetExpiringProducts(DateTime.Now);

            if (await expiredProducts.CountAsync() == 0)
                return NotFound();

            return Ok(await expiredProducts.ToListAsync());
        }

        [HttpPut]
        public async ValueTask<IActionResult> EditProduct(Product product)
        {
            try
            {
                Product editProduct = await
                    productService.UpdateProductAsync(product);

                return Ok(editProduct);
            }
            catch (NullProductException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server error {ex.Message}");
            }
        }

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteProduct(Product product)
        {
            try
            {
                Product deleteProduct = await
                    productService.DeleteProductAsync(product);

                return Ok(deleteProduct);
            }
            catch (NullProductException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server error {ex.Message}");
            }
        }
    }
}