using BuildMaterials.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Core.Contracts
{
    public interface IShoppingCartService
    {
       
        bool ClearCart(string userId);
        bool RemoveFromCart(string userId,int productId);
        bool UpdateCartItem(int productId, string userId, int quantity);
        bool ConfirmOrder(string userId);
        bool AddToCart(int productId, string userId, int quantity);
        List<ShoppingCartItem> GetCartItems(string userId);
    }
}
