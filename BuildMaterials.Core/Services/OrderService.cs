using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildMaterials.Core.Contracts;
using BuildMaterials.Data;
using BuildMaterials.Infrastructure.Data.Domain;

namespace BuildMaterials.Core.Services
{
    public class OrderService :  IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        public OrderService(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public bool Create(int productId, string userId, int quantity)
        {
            var product = this._context.Products.SingleOrDefault(x => x.Id == productId);
            if (product == null)
            {
                return false;
            }



            Order item = new Order(DateTime.Now,
                productId,
                userId,
                product.Price,
                quantity,
                product.Discount

                );
            item.TotalPrice = item.GetTotalPrice(quantity, product.Price, product.Discount);
            product.Quantity -= quantity;
            this._context.Products.Update(product);
            this._context.Orders.Add(item);

          


            return _context.SaveChanges() != 0;
        }

        public Order GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            return this._context.Orders.OrderByDescending(x => x.OrderDate).ToList();
        }

        
        public List<Order> GetOrdersByUser(string userId)
        {
            return this._context.Orders
                .Where(x => x.UserId == userId).
                OrderByDescending(X =>X.OrderDate).ToList();
        }

        public bool RemoveById(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool Update(int orderId, int productId, string userId, int quantity)
        {
            throw new NotImplementedException();
        }
        public bool MyOrders(string userId)
        {
            var orders = this._context.Orders.Where(x => x.UserId == userId).ToList();
            if (orders.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
