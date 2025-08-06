namespace WebApp.Areas.Client.Models
{
    public class OrderProductReqItemsMDL
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }
}
