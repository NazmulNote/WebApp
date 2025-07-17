namespace WebApp.Areas.Admin.Models
{
    public class LocationTreeMDL
    {
        public int ID { get; set; }

        public string? Item { get; set; }
        public int? PId { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Code { get; set; }

        public bool IsActive { get; set; }

        public int InsertId { get; set; }

        public string? InsertedByIP { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
