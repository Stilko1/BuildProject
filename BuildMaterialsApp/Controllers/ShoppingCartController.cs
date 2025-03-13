using BuildMaterials.Core.Contracts;
using BuildMaterialsApp.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuildMaterialsApp.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        // GET: CartController
        public ActionResult Index()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = _cartService.GetCartItems(currentUserId);

            var model = cartItems.Select(item => new ShoppingCartItemIndexVM
            {
                ProductId = item.ProductId,
                ProductName = item.Product.ProductName,
                Picture = item.Product.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount,
                TotalPrice = item.TotalPrice
            }).ToList();

            ViewBag.TotalPrice = Math.Round(model.Sum(item => item.TotalPrice), 2, MidpointRounding.AwayFromZero);

            return View(model);
        }

        // GET: CartController/RemoveFromCart
        public ActionResult RemoveFromCart(int productId)
        {
            return View(productId);
        }

        // POST: CartController/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromCart(int productId, IFormCollection collection)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.RemoveFromCart(currentUserId, productId);
            return RedirectToAction("Index");
        }

        // GET: CartController/ClearCart
        public ActionResult ClearCart()
        {
            return View();
        }

        // POST: CartController/ClearCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClearCart(IFormCollection collection)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.ClearCart(currentUserId);
            return RedirectToAction("Index");
        }

        // GET: CartController/ConfirmOrder
        public ActionResult ConfirmOrder()
        {
            return View();
        }

        // POST: CartController/ConfirmOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmOrder(IFormCollection collection)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.ConfirmOrder(currentUserId);
            return RedirectToAction("MyOrders", "Order");
        }
    }
}