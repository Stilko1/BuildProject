﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildMaterials.Data;
using BuildMaterials.Infrastructure.Data.Domain;

namespace BuildMaterials.Core.Contracts
{
    public interface IOrderService 
    {
       bool Create(int productId, string userId, int quantity);
        List<Order> GetOrders();
        List<Order> GetOrdersByUser(string userId);
        Order GetOrderById(int orderId);
        bool RemoveById(int orderId);
        bool Update(int orderId, int productId, string userId, int quantity);


    }
}
