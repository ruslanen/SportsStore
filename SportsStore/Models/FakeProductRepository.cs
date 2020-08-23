using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> {
            new Product { Name = "Мяч футбольный", Price = 800 },
            new Product { Name = "Мяч волейбольный", Price = 700 },
            new Product { Name = "Ракетка для пинг понга", Price = 500 },
        }.AsQueryable<Product>();
    }
}
