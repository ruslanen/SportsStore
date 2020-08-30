using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;

        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ViewResult List(string category, int productPage = 1)
        {
            return View(
                new ProductsListViewModel
                {
                    Products = productRepository.Products
                        .Where(p => category == null || p.Category == category)
                        .OrderBy(p => p.ProductId)
                        .Skip((productPage - 1) * this.PageSize)
                        .Take(this.PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = this.PageSize,
                        TotalItems = category == null ?
                            this.productRepository.Products.Count() :
                            this.productRepository.Products.Where(x => x.Category == category).Count()
                    },
                    CurrentCategory = category,
                }
            );
        }
    }
}
