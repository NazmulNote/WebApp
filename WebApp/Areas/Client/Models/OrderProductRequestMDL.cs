namespace WebApp.Areas.Client.Models
{
    public class OrderProductRequestMDL
    {
        public int CustomerId { get; set; }
        public string? DeliveryAddress { get; set; }
        public int? CourierChargeId { get; set; }
        public int? PaymentMethodId {  get; set; }
        public decimal? GrandTotal { get; set; }
        public List<OrderProductReqItemsMDL>? Cart { get; set; }
    }
}
