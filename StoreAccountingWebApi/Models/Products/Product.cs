using System;
using System.ComponentModel.DataAnnotations;

namespace StoreAccountingWebApi.Models.Products
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Count must be a positive number")]
        public int Count { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? Manufacturer { get; set; }
    }
}
