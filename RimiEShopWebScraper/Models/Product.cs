using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimiEShopWebScraper.Models
{
    internal class Product
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string? Name { get; set; }
        public long PriceMinorCurrency { get; set; }
        public long PriceOnSaleMinorcurrency { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsOnSale { get; set; } = false;

        public DateTime ScrapeDate { get; set; }

        public SaleType SaleType { get; set; }
        //public ProductCategory Category { get; set; }
        public bool IsProductAvailalbe { get; set; } = true;

    }
}
