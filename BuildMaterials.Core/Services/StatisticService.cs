using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildMaterials.Core.Contracts;
using BuildMaterials.Data;
using BuildMaterials.Infrastructure.Data.Domain;

namespace WebShopApp.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ApplicationDbContext _context;
        public StatisticService(ApplicationDbContext context)
        {
            _context = context;
        }
        public int CountClients()
        {
            return _context.Users.Count()-1;

        }
        public int CountOrders()
        {
            return _context.Orders.Count();
        }
        public int CountProducts()
        {
            return _context.Products.Count();
        }
        public decimal SumOrders()
        {
           var suma = _context.Orders.Sum(x=> x.TotalPrice);

            return suma;
        }




    }
}
