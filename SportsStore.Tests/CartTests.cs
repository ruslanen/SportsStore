using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void CanAddNewLines()
        {
            Product product1 = new Product
            {
                ProductId = 1,
                Name = "P1",
            };
            Product product2 = new Product
            {
                ProductId = 2,
                Name = "P2",
            };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            CartLine[] result = cart.Lines.ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal(product1, result[0].Product);
            Assert.Equal(product2, result[1].Product);
        }

        [Fact]
        public void CanAddQuantityForExistingLines()
        {
            Product product1 = new Product
            {
                ProductId = 1,
                Name = "P1",
            };
            Product product2 = new Product
            {
                ProductId = 2,
                Name = "P2",
            };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 10);
            CartLine[] result = cart.Lines.OrderBy(c => c.Product.ProductId).ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal(11, result[0].Quantity);
            Assert.Equal(1, result[1].Quantity);
        }

        [Fact]
        public void CanRemoveLine()
        {
            Product product1 = new Product
            {
                ProductId = 1,
                Name = "P1",
            };
            Product product2 = new Product
            {
                ProductId = 2,
                Name = "P2",
            };
            Product product3 = new Product
            {
                ProductId = 3,
                Name = "P3",
            };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 3);
            cart.AddItem(product3, 5);
            cart.AddItem(product2, 1);

            cart.RemoveLine(product2);

            Assert.Empty(cart.Lines.Where(c => c.Product == product2));
            Assert.Equal(2, cart.Lines.Count());
        }

        [Fact]
        public void CalculateCartTotal()
        {
            Product product1 = new Product
            {
                ProductId = 1,
                Name = "P1",
                Price = 100M,
            };
            Product product2 = new Product
            {
                ProductId = 2,
                Name = "P2",
                Price = 50M,
            };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);
            cart.AddItem(product1, 3);
            decimal result = cart.ComputeTotalValue();

            Assert.Equal(450M, result);
        }

        [Fact]
        public void CanClearContents()
        {
            Product product1 = new Product
            {
                ProductId = 1,
                Name = "P1",
                Price = 100M,
            };
            Product product2 = new Product
            {
                ProductId = 2,
                Name = "P2",
                Price = 50M,
            };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 1);

            cart.Clear();

            Assert.Empty(cart.Lines);
        }
    }
}
