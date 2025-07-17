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
    }
}
