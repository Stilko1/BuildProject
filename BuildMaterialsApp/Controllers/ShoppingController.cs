using BuildMaterials.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildMaterialsApp.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public ShoppingController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        // GET: ShoppingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShoppingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ShoppingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ShoppingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingController/Delete/5
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
    }
}
