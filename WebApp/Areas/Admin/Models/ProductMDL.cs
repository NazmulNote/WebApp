using System.ComponentModel.DataAnnotations;
using Microsoft.Build.ObjectModelRemoting;

namespace WebApp.Areas.Admin.Models
{
    public class ProductMDL
    {
        public int ID { get; set; }
        public int CatId { get; set; }
        public string? CatName { get; set; }
        public int? SubCatId { get; set; }
        public string? SubCatName { get; set; }
        public int? SubChildCatId { get; set; }
        public string? SubChildCatName { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Specification { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal? SellingPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal? OfferPercent { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal? OfferPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal? Price { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
