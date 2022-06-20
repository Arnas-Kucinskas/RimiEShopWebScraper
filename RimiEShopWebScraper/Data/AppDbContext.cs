using Microsoft.EntityFrameworkCore;
using RimiEShopWebScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimiEShopWebScraper.Data
{
    internal class AppDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning CN in config
                optionsBuilder.UseSqlServer(@"Data Source=localhost,1433;Initial Catalog=RimiScraping;Integrated Security=True;");
            }
        }
    }
}
