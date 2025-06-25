using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace WebApp.Areas.Admin.Models
{
    public class AdminUserMDL
    {
        public int ID { get; set; }
        public long? EmpID { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Role { get; set; } // e.g., SuperAdmin, Admin
        public bool CanInsert { get; set; } = false;
        public bool CanUpdate { get; set; } = false;
        public bool CanDelete { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public int? InsertId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
