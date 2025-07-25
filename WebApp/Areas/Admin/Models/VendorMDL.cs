﻿namespace WebApp.Areas.Admin.Models
{
    public class VendorMDL
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public int? PoliceStationId { get; set; }
        public string? PoliceStationName { get; set; }
        public string? Address { get; set; }
        public string? TIN { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
