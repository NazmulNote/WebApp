using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Areas.Admin.Models
{
    public class ProductPurchaseMDL
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int VendorId { get; set; }
        public string? VendorName { get; set; }
        public int Qty { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal PurchasePrice { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? InvoiceNo { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
        public int InsertId { get; set; }
        public string? InsertedByIP { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
