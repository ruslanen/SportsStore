using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public EFProductRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IQueryable<Product> Products => applicationDbContext.Products;
    }
}
