using System.ComponentModel.DataAnnotations;

namespace BuildMaterialsApp.Models.ShoppingCart
{
    public class ShoppingCartItemIndexVM
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;


        [Display(Name = "Picture")]
        public string? Picture { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }
}