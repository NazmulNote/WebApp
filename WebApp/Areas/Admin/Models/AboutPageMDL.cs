namespace WebApp.Areas.Admin.Models
{
    public class AboutPageMDL
    {
        public int ID { get; set; }
        public string? SliderPhotoUrl { get; set; }
        public IFormFile? SliderPhoto { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? AboutPhotoUrl { get; set; }
        public IFormFile? AboutPhoto { get; set; }
        public string? Vision { get; set; }
        public string? Mission { get; set; }
        public string? Achievements { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
