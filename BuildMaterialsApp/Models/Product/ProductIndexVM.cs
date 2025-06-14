﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BuildMaterialsApp.Models.Product
{
    public class ProductIndexVM
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;
        public string ProductId { get; set; } = null!;

        public int QuantityInStock { get; set; }
        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; } = null!;
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Discount")]
        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
