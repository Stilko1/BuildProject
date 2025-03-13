using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Infrastructure.Data.Domain
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        [Required]
        public int ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; private set; }
    }
}
