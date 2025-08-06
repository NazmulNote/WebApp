using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Areas.Admin.Models
{
    public class ServiceMDL
    {
        public int ID { get; set; }
        public int ServiceCatId { get; set; }
        public string? ServiceCat { get; set; }
        public string? Name { get; set; }
        public string? ShortDesc { get; set; }
        public string? FullDesc { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile? Photo { get; set; }
        public string? VideoUrl { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
