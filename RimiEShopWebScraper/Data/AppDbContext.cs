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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
           
        //    modelBuilder.Entity<Product>()
        //        .Property(p => p.ScrapeDate)
        //        .HasColumnType("date");
        //    base.OnModelCreating(modelBuilder);

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
#warning CN in config
                //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RimiScraping;Integrated Security=SSPI;");
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-6QJ1MGC;Initial Catalog=RimiScraping;Integrated Security=SSPI;");
            }
        }
    }
}
