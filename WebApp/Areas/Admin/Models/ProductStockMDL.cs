namespace WebApp.Areas.Admin.Models
{
    public class ProductStockMDL
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public decimal AveragePrice { get; set; }
        public int Quantity { get; set; }

        public string? Name { get; set; }
        public string? ProductName { get; set; }

        public int CatId { get; set; }
        public string? CatName { get; set; }

        public int? SubCatId { get; set; }
        public string? SubCatName { get; set; }

        public int? SubChildCatId { get; set; }
        public string? SubChildCatName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
