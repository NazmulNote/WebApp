namespace WebApp.Areas.Admin.Models
{
    public class OrderProductItemMDL
    {
        public int ID { get; set; }

        public int OrderProductId { get; set; }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPhotoUrl { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        public bool IsActive { get; set; }

        public int InsertId { get; set; }

        public string? InsertedByIP { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
