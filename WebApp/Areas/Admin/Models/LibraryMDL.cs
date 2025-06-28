using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Admin.Models
{
    public class LibraryMDL
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string? Item { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public int? SubCatId { get; set; }

        public string? SubCatName { get; set; }
        public int? SubChildCatId { get; set; }
        public string? SubChildCatName { get; set; }
        public string? ItemName { get; set; }
        public string? ItemCode { get; set; }

        public string? Description { get; set; }

        public IFormFile? Photo { get; set; }
        public string? PhotoUrl { get; set; }

        public int InsertId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
