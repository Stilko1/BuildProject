using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Infrastructure.Data.Domain
{
    public class Order
    {
        public Order() { }

        private decimal GetTotalPrice()
        {
            return Quantity * (Price - Price * Discount / 100);
        }
        public decimal GetTotalPrice(int quantity, decimal price, decimal discount)
        {
            return quantity * (price - price * discount / 100);
        }

        public Order(DateTime orderdate,
            int productId,
            string userId,
            decimal price,
            int quantity,
            decimal discount)
        {
            OrderDate = orderdate;
            ProductId = productId;
            UserId = userId;
            Price = price;
            Quantity = quantity;
            Discount = discount;
            TotalPrice = GetTotalPrice();
        }
        public int Id { get; set; }
        [Required]
        
        public DateTime OrderDate { get; set; }
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; private set; }

    }
}
