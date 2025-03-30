using AspNetCoreGeneratedDocument;
using System.ComponentModel.DataAnnotations;

namespace BuildMaterialsApp.Models.Order
{
    public class OrderIndexVM
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Product { get; set; } = null!;
        public int Quantity { get; set; }
        public int QuantityInStock { get; set; }
        public string User { get; set; }
        public string OrderDate { get; set; }

        public string Picture { get; set; }
        [Range(1, 100)]
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        Views_Order_MyOrders MyOrders { get; set; }

    }
}

