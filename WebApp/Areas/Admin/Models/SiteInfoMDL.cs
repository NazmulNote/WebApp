namespace WebApp.Areas.Admin.Models
{
    public class SiteInfoMDL
    {
        public int ID { get; set; }
        public string? HAboutTitle { get; set; }
        public string? HAboutDescription { get; set; }
        public string? HAboutPhotoUrl { get; set; }
        public IFormFile? HAboutPhoto { get; set; }
        public string? HAboutUrl { get; set; }
        public string? ContactSliderUrl { get; set; }
        public IFormFile? ContactSlider { get; set; }
        public string? ContactShortDesc { get; set; }
        public string? ContactMap { get; set; }
        public string? ClientSliderUrl { get; set; }
        public IFormFile? ClientSlider { get; set; }
        public string? ClientSliderTitle { get; set; }
        public string? ClientSliderDesc { get; set; }
        public string? ClientTitle { get; set; }
        public string? ClientDesc { get; set; }
        public string? ServiceSliderUrl { get; set; }
        public IFormFile? ServiceSlider { get; set; }
        public string? ServiceSliderTitle { get; set; }
        public string? ServiceSliderDesc { get; set; }
        public string? ServiceTitle { get; set; }
        public string? ServiceDesc { get; set; }
        public string? ServiceDetailsDesc { get; set; }
        public bool IsActive { get; set; } = true;
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}