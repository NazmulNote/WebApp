namespace WebApp.Areas.Admin.Models
{
    public class CompanyInfoMDL
    {
        public int ID { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyCode { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? EmailPassword { get; set; }
        public string? Phone { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? YouTubeUrl { get; set; }
        public string? Website { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
