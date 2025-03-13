using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using BuildMaterials.Core.Contracts;
using BuildMaterials.Infrastructure.Data.Domain;
using BuildMaterialsApp.Models.Order;
using Microsoft.AspNetCore.Cors.Infrastructure;


namespace BuildMaterials.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _cartService;

        public OrderController(IProductService productService, IOrderService orderService, IShoppingCartService cartService)
        {
            _productService = productService;
            _orderService = orderService;
            _cartService = cartService;
        }
        // GET: OrderController
        //[Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            List<OrderIndexVM> orders = _orderService.GetOrders()
                 .Select(x => new OrderIndexVM
                 {
                     Id = x.Id,
                     OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                     ProductId = x.ProductId,
                     Product = x.Product.ProductName,
                     UserId = x.UserId,
                     Quantity = x.Quantity,
                     Price = x.Price,
                     Discount = x.Discount,
                     Picture = x.Product.Picture,
                     User = x.User.UserName,
                      TotalPrice = x.TotalPrice
                 }).ToList();
            return View(orders);

        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create(int id)
        {
            Product product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            OrderCreateVM order = new OrderCreateVM()
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                QuantityInStock = product.Quantity,
                Price = product.Price,
                Discount = product.Discount,
                Picture = product.Picture,
            };

            return View(order);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreateVM bindingModel)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = this._productService.GetProductById(bindingModel.ProductId);

            if (currentUserId == null || product == null || product.Quantity < bindingModel.Quantity || product.Quantity == 0)
            {
                return RedirectToAction("Denied", "Order");
            }

            if (ModelState.IsValid)
            {
                _cartService.AddToCart(bindingModel.ProductId, currentUserId, bindingModel.Quantity);
            }

            return this.RedirectToAction("Index", "ShoppingCart");
        }

        // GET: OrderController/Edit
        public ActionResult Edit(int productId)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = _cartService.GetCartItems(currentUserId).FirstOrDefault(x => x.ProductId == productId);

            if (cartItem == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(productId);

            var model = new OrderCreateVM
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                QuantityInStock = product.Quantity,
                Picture = product.Picture,
                Quantity = cartItem.Quantity,
                Price = product.Price,
                Discount = product.Discount,
                TotalPrice = cartItem.TotalPrice
            };

            return View(model);
        }

        // POST: OrderController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.UpdateCartItem(model.ProductId, currentUserId, model.Quantity);

            return RedirectToAction("Index", "ShoppingCart");
        }


        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Denied(int id)
        {
            return View();
        }
        public ActionResult MyOrders()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<OrderIndexVM> orders = _orderService.GetOrdersByUser(currentUserId)
                .Select(x => new OrderIndexVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    UserId = x.UserId,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    Picture = x.Product.Picture,
                    User = x.User.UserName,
                }).ToList();
            return View(orders);
        }
    }
}
