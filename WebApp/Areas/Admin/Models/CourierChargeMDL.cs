namespace WebApp.Areas.Admin.Models
{
    public class CourierChargeMDL
    {
        public int ID { get; set; }
        public string? Location { get; set; }
        public int ChargeAmount { get; set; }
        public int EstimatedDays { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
