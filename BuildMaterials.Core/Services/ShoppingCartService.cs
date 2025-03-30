using BuildMaterials.Core.Contracts;
using BuildMaterials.Data;
using BuildMaterials.Infrastructure.Data.Domain;
using BuildMaterialsApp.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {

        private readonly ApplicationDbContext _context;

        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ShoppingCartItem> GetCartItems(string userId)
        {
            return _context.ShoppingCartItems.Include(x => x.Product).Where(x => x.ShoppingCart.UserId == userId).ToList();
        }
     
        public bool ClearCart(string userId)
        {
            var cartItems = _context.ShoppingCartItems.Include(x => x.ShoppingCart).Where(x => x.ShoppingCart.UserId == userId).ToList();

            _context.ShoppingCartItems.RemoveRange(cartItems);
            return _context.SaveChanges() != 0;
        }

        public bool ConfirmOrder(string userId)
        {
            var cart = _context.ShoppingCarts.Include(x => x.ShoppingCartItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId);

            if (cart == null || !cart.ShoppingCartItems.Any())
            {
                return false;
            }

            var orders = new List<Order>();

            foreach (var item in cart.ShoppingCartItems)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product == null || product.Quantity < item.Quantity)
                {
                    continue;
                }

                product.Quantity -= item.Quantity;
                _context.Products.Update(product);

                orders.Add(new Order
                {
                    OrderDate = DateTime.Now,
                    ProductId = item.ProductId,
                    UserId = userId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Discount = item.Discount,
                });
            }

            if (!orders.Any())
            {
                return false;
            }
            else
            {
                _context.Orders.AddRange(orders);
                _context.ShoppingCartItems.RemoveRange(cart.ShoppingCartItems);
                return _context.SaveChanges() != 0;
            }
        }

        public bool RemoveFromCart(string userId, int productId)
        {
            var cartItem = _context.ShoppingCartItems.Include(x => x.ShoppingCart).FirstOrDefault(x => x.ShoppingCart.UserId == userId && x.ProductId == productId);

            if (cartItem == null)
            {
                return false;
            }
            _context.ShoppingCartItems.Remove(cartItem);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateCartItem(int productId, string userId, int quantity)
        {
            var cartItem = _context.ShoppingCartItems.Include(x => x.ShoppingCart).FirstOrDefault(x => x.ShoppingCart.UserId == userId && x.ProductId == productId);

            if (cartItem == null)
            {
                return false;
            }
            cartItem.Quantity = quantity;
            return _context.SaveChanges() != 0;
        }

        public bool AddToCart(int productId, string userId, int quantity)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(x => x.UserId == userId);
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId
                };
                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges();
            }

            var product = _context.Products.Find(productId);
            if (product == null || product.Quantity < quantity)
            {
                return false;
            }

            var cartItem = _context.ShoppingCartItems.FirstOrDefault(x => x.ShoppingCartId == cart.Id && x.ProductId == productId);

            if (cartItem != null && cartItem.Quantity >= product.Quantity)
            {
                return false;
            }

            if (cartItem == null)
            {
                int finalQuantity = Math.Min(quantity, 100);

                cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = finalQuantity,
                    Price = product.Price,
                    Discount = product.Discount,
                    TotalPrice = finalQuantity * (product.Price - product.Price * product.Discount / 100)

                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                int newQuantity = cartItem.Quantity + quantity;

                if (newQuantity > product.Quantity)
                {
                    cartItem.Quantity = product.Quantity;
                }
                else
                {
                    cartItem.Quantity = Math.Min(newQuantity, 100);
                }
                cartItem.TotalPrice = cartItem.Quantity * (product.Price - product.Price * product.Discount / 100);
            }

            return _context.SaveChanges() != 0;
        }
    }
}