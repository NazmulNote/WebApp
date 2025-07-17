using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class AboutPageController : Controller
    {
        private readonly AboutPageData _aboutPageData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AboutPageController(IWebHostEnvironment webHostEnvironment)
        {
            _aboutPageData = new AboutPageData();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult AboutPageCreate()
        {
            return View();
        }
        public IActionResult GetAboutPageHdrPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.AboutPage = _aboutPageData.GetAboutPageInfo();
                if (viewModel.AboutPage.ID != 0)
                {
                    return PartialView("_GetAboutPageHdrPView", viewModel);
                }
                else
                {
                    viewModel.AboutPage = new AboutPageMDL();
                    return PartialView("_GetAboutPageHdrPView", viewModel);
                }
            }
            catch (Exception ex) { }
            return PartialView("_GetAboutPageHdrPView", viewModel);
        }
        [HttpPost]
        public IActionResult AboutPageSetUpdate(AdminViewModel viewModel, IFormFile ImageFile1, IFormFile ImageFile2)
        {
            try
            {
                if (viewModel != null)
                {
                    AboutPageMDL aboutPage = new AboutPageMDL();
                    AboutPageMDL existAboutPage = _aboutPageData.GetAboutPageInfo();

                    if (existAboutPage.ID == 0) // Insert
                    {
                        aboutPage.Title = viewModel.AboutPage.Title;
                        aboutPage.Description = viewModel.AboutPage.Description;
                        aboutPage.Vision = viewModel.AboutPage.Vision;
                        aboutPage.Mission = viewModel.AboutPage.Mission;
                        aboutPage.Achievements = viewModel.AboutPage.Achievements;
                        aboutPage.IsActive = true;

                        if (ImageFile1 != null && ImageFile1.Length > 0)
                            aboutPage.SliderPhotoUrl = UploadImage("SliderPhoto", ImageFile1);

                        if (ImageFile2 != null && ImageFile2.Length > 0)
                            aboutPage.AboutPhotoUrl = UploadImage("AboutPhoto", ImageFile2);

                        aboutPage.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        aboutPage.InsertedByIP = HttpContext.Connection.RemoteIpAddress?.ToString();

                        var result = _aboutPageData.AboutPageInsertUpdate(aboutPage, "Insert");
                        return Json(result.ID);
                    }
                    else // Update
                    {
                        aboutPage.ID = viewModel.AboutPage.ID;
                        aboutPage.Title = viewModel.AboutPage.Title;
                        aboutPage.Description = viewModel.AboutPage.Description;
                        aboutPage.Vision = viewModel.AboutPage.Vision;
                        aboutPage.Mission = viewModel.AboutPage.Mission;
                        aboutPage.Achievements = viewModel.AboutPage.Achievements;
                        aboutPage.IsActive = true;

                        // Handle SliderPhotoUrl
                        if (ImageFile1 != null && ImageFile1.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.AboutPage.SliderPhotoUrl))
                            {
                                var oldSliderPath = Path.Combine(_webHostEnvironment.WebRootPath, "image", viewModel.AboutPage.SliderPhotoUrl);
                                if (System.IO.File.Exists(oldSliderPath))
                                {
                                    System.IO.File.Delete(oldSliderPath);
                                }
                            }
                            aboutPage.SliderPhotoUrl = UploadImage("SliderPhoto", ImageFile1);
                        }
                        else
                        {
                            aboutPage.SliderPhotoUrl = viewModel.AboutPage.SliderPhotoUrl;
                        }

                        // Handle AboutPhotoUrl
                        if (ImageFile2 != null && ImageFile2.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.AboutPage.AboutPhotoUrl))
                            {
                                var oldAboutPath = Path.Combine(_webHostEnvironment.WebRootPath, "image", viewModel.AboutPage.AboutPhotoUrl);
                                if (System.IO.File.Exists(oldAboutPath))
                                {
                                    System.IO.File.Delete(oldAboutPath);
                                }
                            }
                            aboutPage.AboutPhotoUrl = UploadImage("AboutPhoto", ImageFile2);
                        }
                        else
                        {
                            aboutPage.AboutPhotoUrl = viewModel.AboutPage.AboutPhotoUrl;
                        }

                        aboutPage.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        var result = _aboutPageData.AboutPageInsertUpdate(aboutPage, "Update");
                        return Json(result.ID);
                    }
                }
            }
            catch (Exception ex){}
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
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/img/about");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            imageName = $"../Admin/img/about/{fileName}";
            return imageName;
        }
    }
}
