using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimiEShopWebScraper.Data
{
    public class ProductsRepository
    {
        private readonly AppDbContext _context;
        internal ProductsRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }


    }
}
