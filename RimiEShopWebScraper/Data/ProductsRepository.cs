using Microsoft.EntityFrameworkCore;
using RimiEShopWebScraper.Models;
using RimiEShopWebScraper.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimiEShopWebScraper.Data
{
    internal class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;
        public ProductsRepository(IDbContextFactory<AppDbContext> appDbContext)
        {
            _context = appDbContext.CreateDbContext();
        }

        public async Task BatchInsertProducts(Dictionary<long, Product> products)
        {
            await _context.Products.AddRangeAsync(products.Values);
            await _context.SaveChangesAsync();
        }


    }
}
