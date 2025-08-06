namespace WebApp.Areas.Client.Models
{
    public class SubCatMDL
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public List<SubChildCatMDL>? SubChildCatList { get; set; }
    }
}
