using WebApp.Areas.Admin.Models;
namespace WebApp.Areas.Admin.Models
{
    public class AdminViewModel
    {
        public AdminUserMDL? AdminUser { get; set; }
        public List<AdminUserMDL>? AdminUserList { get; set; }
        public LibraryMDL? Library { get; set; }
        public List<LibraryMDL>? LibraryList { get; set; }
    }
}
