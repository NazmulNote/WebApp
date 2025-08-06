using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class SiteInfoController : Controller
    {
        private readonly SiteInfoData _SiteInfoData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SiteInfoController(IWebHostEnvironment webHostEnvironment)
        {
            _SiteInfoData = new SiteInfoData();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult SiteInfoCreate()
        {
            return View();
        }
        public IActionResult GetSiteInfoHdrPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.SiteInfo = _SiteInfoData.GetSiteInfo();
                if (viewModel.SiteInfo.ID != 0)
                {
                    return PartialView("_GetSiteInfoHdrPView", viewModel);
                }
                else
                {
                    viewModel.SiteInfo = new SiteInfoMDL();
                    return PartialView("_GetSiteInfoHdrPView", viewModel);
                }
            }
            catch (Exception ex) { }
            return PartialView("_GetSiteInfoHdrPView", viewModel);
        }
        [HttpPost]
        public IActionResult SiteInfoSetUpdate(AdminViewModel viewModel, IFormFile HAboutPhotoUrl,IFormFile ContactSliderUrl,
            IFormFile ClientSliderUrl,IFormFile ServiceSliderUrl)
        {
            try
            {
                if (viewModel != null)
                {
                    SiteInfoMDL SiteInfo = new SiteInfoMDL();
                    SiteInfoMDL existSiteInfo = _SiteInfoData.GetSiteInfo();

                    if (existSiteInfo.ID == 0)
                    {
                        SiteInfo.HAboutTitle = viewModel.SiteInfo.HAboutTitle;
                        SiteInfo.HAboutDescription = viewModel.SiteInfo.HAboutDescription;
                        SiteInfo.HAboutUrl = viewModel.SiteInfo.HAboutUrl;
                        SiteInfo.ContactShortDesc = viewModel.SiteInfo.ContactShortDesc;
                        SiteInfo.ContactMap = viewModel.SiteInfo.ContactMap;
                        SiteInfo.ClientSliderTitle = viewModel.SiteInfo.ClientSliderTitle;
                        SiteInfo.ClientSliderDesc = viewModel.SiteInfo.ClientSliderDesc;
                        SiteInfo.ClientTitle = viewModel.SiteInfo.ClientTitle;
                        SiteInfo.ClientDesc = viewModel.SiteInfo.ClientDesc;
                        SiteInfo.ServiceSliderTitle = viewModel.SiteInfo.ServiceSliderTitle;
                        SiteInfo.ServiceSliderDesc = viewModel.SiteInfo.ServiceSliderDesc;
                        SiteInfo.ServiceTitle = viewModel.SiteInfo.ServiceTitle;
                        SiteInfo.ServiceDesc = viewModel.SiteInfo.ServiceDesc;
                        SiteInfo.ServiceDetailsDesc = viewModel.SiteInfo.ServiceDetailsDesc;
                        SiteInfo.IsActive = true;

                        #region HAboutPhotoUrl-------------------------------
                        if (HAboutPhotoUrl != null && HAboutPhotoUrl.Length > 0)
                        {
                            SiteInfo.HAboutPhotoUrl = UploadImage("HomeAbout", HAboutPhotoUrl);
                        }
                        #endregion
                        #region ContactSliderUrl ----------------------------
                        if (ContactSliderUrl != null && ContactSliderUrl.Length > 0)
                        {
                            SiteInfo.ContactSliderUrl = UploadImage("ContactSlider", ContactSliderUrl);
                        }
                        #endregion
                        #region ClientSliderUrl------------------------------
                        if (ClientSliderUrl != null && ClientSliderUrl.Length > 0)
                        {
                            SiteInfo.ClientSliderUrl = UploadImage("ClientSlider", ClientSliderUrl);
                        }
                        #endregion
                        #region ServiceSliderUrl------------------------------
                        if (ServiceSliderUrl != null && ServiceSliderUrl.Length > 0)
                        {
                            SiteInfo.ServiceSliderUrl = UploadImage("ServiceSlider", ServiceSliderUrl);
                        }
                        #endregion

                        SiteInfo.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        var result = _SiteInfoData.SiteInfoInsertUpdate(SiteInfo, "Insert");
                        return Json(result.ID);
                    }
                    else
                    {
                        SiteInfo.ID = viewModel.SiteInfo.ID;
                        SiteInfo.HAboutTitle = viewModel.SiteInfo.HAboutTitle;
                        SiteInfo.HAboutDescription = viewModel.SiteInfo.HAboutDescription;
                        SiteInfo.HAboutUrl = viewModel.SiteInfo.HAboutUrl;
                        SiteInfo.ContactShortDesc = viewModel.SiteInfo.ContactShortDesc;
                        SiteInfo.ContactMap = viewModel.SiteInfo.ContactMap;
                        SiteInfo.ClientSliderTitle = viewModel.SiteInfo.ClientSliderTitle;
                        SiteInfo.ClientSliderDesc = viewModel.SiteInfo.ClientSliderDesc;
                        SiteInfo.ClientTitle = viewModel.SiteInfo.ClientTitle;
                        SiteInfo.ClientDesc = viewModel.SiteInfo.ClientDesc;
                        SiteInfo.ServiceSliderTitle = viewModel.SiteInfo.ServiceSliderTitle;
                        SiteInfo.ServiceSliderDesc = viewModel.SiteInfo.ServiceSliderDesc;
                        SiteInfo.ServiceTitle = viewModel.SiteInfo.ServiceTitle;
                        SiteInfo.ServiceDesc = viewModel.SiteInfo.ServiceDesc;
                        SiteInfo.ServiceDetailsDesc = viewModel.SiteInfo.ServiceDetailsDesc;
                        SiteInfo.IsActive = true;

                        #region HAboutPhotoUrl-----------------------------------1
                        if (HAboutPhotoUrl != null && HAboutPhotoUrl.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.SiteInfo.HAboutPhotoUrl))
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "../" + viewModel.SiteInfo.HAboutPhotoUrl);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }
                            SiteInfo.HAboutPhotoUrl = UploadImage("HomeAbout", HAboutPhotoUrl);
                        }
                        else
                        {
                            SiteInfo.HAboutPhotoUrl = viewModel.SiteInfo.HAboutPhotoUrl;
                        }
                        #endregion
                        #region ContactSliderUrl--------------------------------2
                        if (ContactSliderUrl != null && ContactSliderUrl.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.SiteInfo.ContactSliderUrl))
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "../" + viewModel.SiteInfo.ContactSliderUrl);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }
                            SiteInfo.ContactSliderUrl = UploadImage("ContactSlider", ContactSliderUrl);
                        }
                        else
                        {
                            SiteInfo.ContactSliderUrl = viewModel.SiteInfo.ContactSliderUrl;
                        }
                        #endregion
                        #region ClientSliderUrl--------------------------------3
                        if (ClientSliderUrl != null && ClientSliderUrl.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.SiteInfo.ClientSliderUrl))
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "../" + viewModel.SiteInfo.ClientSliderUrl);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }
                            SiteInfo.ClientSliderUrl = UploadImage("ClientSlider", ClientSliderUrl);
                        }
                        else
                        {
                            SiteInfo.ClientSliderUrl = viewModel.SiteInfo.ClientSliderUrl;
                        }
                        #endregion
                        #region ServiceSliderUrl--------------------------------4
                        if (ServiceSliderUrl != null && ServiceSliderUrl.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.SiteInfo.ServiceSliderUrl))
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "../" + viewModel.SiteInfo.ServiceSliderUrl);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }
                            SiteInfo.ServiceSliderUrl = UploadImage("ServiceSlider", ServiceSliderUrl);
                        }
                        else
                        {
                            SiteInfo.ServiceSliderUrl = viewModel.SiteInfo.ServiceSliderUrl;
                        }
                        #endregion
                        SiteInfo.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        var result = _SiteInfoData.SiteInfoInsertUpdate(SiteInfo, "Update");
                        return Json(result.ID);
                    }
                }
            }
            catch (Exception ex){ }
            return Json(0);
        }
        public string UploadImage(string userName, IFormFile ImageFile)
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                throw new ArgumentException("File is not selected.");
            }
            string imageName = string.Empty;
            string fileName = userName + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(ImageFile.FileName);
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/img/home");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            imageName = $"/Admin/img/home/{fileName}";
            return imageName;
        }
    }
}
