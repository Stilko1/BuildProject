using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Infrastructure.Data.Domain
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    }   
}