namespace WebApp.Areas.Admin.Models
{
    public class OrderProductMDL
    {
        public int ID { get; set; }
        public long? OrderNo { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Status { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? DeliveryAddress { get; set; }
        public int? CourierChargeId { get; set; }
        public int? PaymentMethodId { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public List<OrderProductItemMDL>? OrderProductItemList { get; set; }
    }
}
