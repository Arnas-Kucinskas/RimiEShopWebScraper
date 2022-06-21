using RimiEShopWebScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimiEShopWebScraper.Repository.Interfaces
{
    internal interface IProductsRepository
    {
        Task BatchInsertProducts(Dictionary<long, Product> products);
    }
}
