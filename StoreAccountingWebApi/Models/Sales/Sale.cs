using StoreAccountingWebApi.Models.Products;
using StoreAccountingWebApi.Models.Users;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StoreAccountingWebApi.Models.Sales
{
    public class Sale
    {
        public int SaleId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public List<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
    }

    public class SaleProduct
    {
        public int SaleProductId { get; set; }
        public int SaleId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtSale { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        [JsonIgnore]
        public Sale Sale { get; set; }
    }
}
