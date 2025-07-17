namespace WebApp.Areas.Admin.Models
{
    public class SliderMDL
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public IFormFile? Photo { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string InsertedByIP { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; }
    }
}
