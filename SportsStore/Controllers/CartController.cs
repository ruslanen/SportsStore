using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SportsStore.Models;
using Newtonsoft.Json;
using SportsStore.Infrastructure;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository productRepository;

        public CartController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart cart = GetCart() ?? new Cart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = productRepository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = this.GetCart(),
                ReturnUrl = returnUrl,
            });
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetJson<Cart>("Cart");
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
