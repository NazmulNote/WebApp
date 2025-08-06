using WebApp.Areas.Admin.Models;
namespace WebApp.Areas.Client.Models
{
    public class ClientViewModel
    {
        public SliderMDL? Slider {  get; set; }
        public List<SliderMDL>? SliderList { get; set; }
        public CompanyInfoMDL? CompanyInfo { get; set; }
        public SiteInfoMDL? SiteInfo { get; set; }
        public AboutPageMDL? AboutPage { get; set; }
        public List<TeamMemberMDL>? TeamMemberList { get; set; }
        public MessageMDL? Message { get; set; }
        public ClientMDL? Client { get; set; }
        public List<ClientMDL>? ClientList { get; set; }
        public ServiceMDL? Service { get; set; }
        public List<ServiceMDL>? ServiceList { get; set; }
        public ProductMDL? Product { get; set; }
        public List<ProductMDL>? ProductList { get; set; }
        public SubCatMDL? SubCat { get; set; }
        public List<SubCatMDL>? SubCatList { get; set; }
        public List<CatMDL>? CatList { get; set; }
        public CustomerMDL? Customer { get; set; }
        public List<CustomerMDL>? CustomerList { get; set; }
        public CourierChargeMDL? CourierCharge { get; set; }
        public List<CourierChargeMDL>? CourierChargeList { get; set; }
        public OrderProductRequestMDL? OrderProductRequest { get; set; }
        public List<OrderProductRequestMDL>? OrderProductRequestList { get; set; }
        public OrderProductReqItemsMDL? OrderProductReqItems { get; set; }
        public List<OrderProductReqItemsMDL>? OrderProductReqItemsList { get; set; }
        public OrderProductMDL? OrderProduct { get; set; }
        public List<OrderProductMDL>? OrderProductList { get; set; }
        public OrderProductItemMDL? OrderProductItem { get; set; }
        public List<OrderProductItemMDL>? OrderProductItemList { get; set; }
    }
}
