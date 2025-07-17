using System.Security.Cryptography.Pkcs;
using WebApp.Areas.Admin.Models;
namespace WebApp.Areas.Admin.Models
{
    public class AdminViewModel
    {
        public AdminUserMDL? AdminUser { get; set; }
        public List<AdminUserMDL>? AdminUserList { get; set; }
        public LibraryMDL? Library { get; set; }
        public List<LibraryMDL>? LibraryList { get; set; }
        public ProductMDL? Product { get; set; }
        public List<ProductMDL>? ProductList { get; set; }
        public VendorMDL? Vendor { get; set; }
        public List<VendorMDL>? VendorList { get; set; }
        public ProductPurchaseMDL? ProductPurchase { get; set; }
        public List<ProductPurchaseMDL>? ProductPurchaseList { get; set; }
        public LocationTreeMDL? LocationTree { get; set; }
        public List<LocationTreeMDL>? LocationTreeList { get; set; }
        public ProductStockMDL? ProductStock { get; set; }
        public List<ProductStockMDL>? ProductStockList { get; set; }
        public CustomerMDL? Customer { get; set; }
        public List<CustomerMDL>? CustomerList { get; set; }
        public SliderMDL? Slider { get; set; }
        public List<SliderMDL>? SliderList { get; set; }
        public CompanyInfoMDL? CompanyInfo { get; set; }
        public List<CompanyInfoMDL>? CompanyInfoList { get; set; }
        public SiteInfoMDL? SiteInfo { get; set; }
        public List<SiteInfoMDL>? SiteInfoList { get; set; }
        public AboutPageMDL? AboutPage { get; set; }
        public List<AboutPageMDL>? AboutPageList { get; set ; }
        public TeamMemberMDL? TeamMember { get; set; }
        public List<TeamMemberMDL>? TeamMemberList { get; set; }
        public MessageMDL? Message { get; set; }
        public List <MessageMDL>? MessageList { get;set; }
    }
}
