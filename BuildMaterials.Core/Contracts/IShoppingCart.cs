using BuildMaterials.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Core.Contracts
{
    public interface IShoppingCart
    {
        bool Create(int productId, string userId, int quantity);
        List<ShoppingCart> GetOrders();
        List<ShoppingCart> GetOrdersByUser(string userId);
        Order GetOrderById(int orderId);
        bool RemoveById(int orderId);
        bool Update(int orderId, int productId, string userId, int quantity);
    }
}
