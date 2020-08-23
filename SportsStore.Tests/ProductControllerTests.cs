using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        // The AAA (Arrange, Act, Assert)
        [Fact]
        public void CanPaginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductId = 1, Name = "P1" },
                new Product { ProductId = 2, Name = "P2" },
                new Product { ProductId = 3, Name = "P3" },
                new Product { ProductId = 4, Name = "P4" },
                new Product { ProductId = 5, Name = "P5" },
            }).AsQueryable<Product>());
            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 3;

            ProductsListViewModel result = productController.List(2).ViewData.Model as ProductsListViewModel;

            Product[] products = result.Products.ToArray();
            Assert.True(products.Length == 2);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
        }

        [Fact]
        public void CanSendPaginationViewModel()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductId = 1, Name = "P1" },
                new Product { ProductId = 2, Name = "P2" },
                new Product { ProductId = 3, Name = "P3" },
                new Product { ProductId = 4, Name = "P4" },
                new Product { ProductId = 5, Name = "P5" },
            }).AsQueryable<Product>());

            var productController = new ProductController(mock.Object);
            var result = productController.List(2).ViewData.Model as ProductsListViewModel;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(4, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }
    }
}
