namespace WebApp.Areas.Admin.Models
{
    public class TeamMemberMDL
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile? Photo { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? PresentAddress { get; set; }
        public string? PermanentAddress { get; set; }

        public string? NID { get; set; }
        public string? EmployeeCode { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Nationality { get; set; }

        public string? Department { get; set; }
        public string? TeamCategory { get; set; }
        public string? Skills { get; set; }
        public string? ShortBio { get; set; }

        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Twitter { get; set; }
        public int? ViewOrder { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
