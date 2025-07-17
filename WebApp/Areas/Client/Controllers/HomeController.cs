using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Areas.Client.Data;
using WebApp.Areas.Client.Models;

namespace WebApp.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly HomeData _homeData;
        public HomeController()
        {
            _homeData = new HomeData();
        }
        [HttpGet]
        public IActionResult Index()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.SliderList = _homeData.GetSliderList();
                viewModel.SiteInfo = _homeData.GetSiteInfo();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.CompanyInfo = _homeData.GetCompanyInfo();
                viewModel.SiteInfo = _homeData.GetSiteInfo();
            }catch(Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult About()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                viewModel.CompanyInfo = _homeData.GetCompanyInfo();
                viewModel.AboutPage = _homeData.GetAboutPageInfo();
                viewModel.TeamMemberList = _homeData.GetTeamMemberList();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult MessageSetUpdate(ClientViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.Message != null)
                {
                    MessageMDL message = new MessageMDL();

                    if (viewModel.Message.ID == 0)
                    {
                        // Insert
                        message.Type = "Message";
                        message.Name = viewModel.Message.Name;
                        message.Type = viewModel.Message.Type;
                        message.Email = viewModel.Message.Email;
                        message.Subject = viewModel.Message.Subject;
                        message.Body = viewModel.Message.Body;
                        message.IsActive = viewModel.Message.IsActive;
                        message.InsertId = 0;
                        message.InsertedByIP = HttpContext.Connection.RemoteIpAddress?.ToString();

                        var result = _homeData.MessageInsertUpdate(message, "Insert");
                        return Json(result.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

            return Json(0);
        }
    }
}
