using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class CompanyInfoController : Controller
    {
        private readonly CompanyInfoData _companyInfoData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyInfoController(IWebHostEnvironment webHostEnvironment)
        {
            _companyInfoData = new CompanyInfoData();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult CompanyInfoCreate()
        {
            return View();
        }
        public IActionResult GetCompanyInfoHdrPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.CompanyInfo = _companyInfoData.GetCompanyInfo();
                if (viewModel.CompanyInfo.ID != 0)
                {
                    return PartialView("_GetCompanyInfoHdrPView", viewModel);
                }
                else
                {
                    viewModel.CompanyInfo = new CompanyInfoMDL();
                    return PartialView("_GetCompanyInfoHdrPView", viewModel);
                }
            }
            catch (Exception ex) { }
            return PartialView("_GetCompanyInfoHdrPView", viewModel);
        }
        [HttpPost]
        public IActionResult CompanyInfoSetUpdate(AdminViewModel viewModel, IFormFile ImageFile)
        {
            try
            {
                if (viewModel != null)
                {
                    CompanyInfoMDL company = new CompanyInfoMDL();
                    CompanyInfoMDL existCompanyInfo =  _companyInfoData.GetCompanyInfo();
                    if (existCompanyInfo.ID == 0)
                    {
                        company.CompanyName = viewModel.CompanyInfo.CompanyName;
                        company.CompanyCode = viewModel.CompanyInfo.CompanyCode;
                        company.Description = viewModel.CompanyInfo.Description;
                        company.Address = viewModel.CompanyInfo.Address;
                        company.Email = viewModel.CompanyInfo.Email;
                        company.Phone = viewModel.CompanyInfo.Phone;
                        company.FacebookUrl = viewModel.CompanyInfo.FacebookUrl;
                        company.TwitterUrl = viewModel.CompanyInfo.TwitterUrl;
                        company.LinkedInUrl = viewModel.CompanyInfo.LinkedInUrl;
                        company.YouTubeUrl = viewModel.CompanyInfo.YouTubeUrl;
                        company.Website = viewModel.CompanyInfo.Website;
                        company.IsActive = true;

                        if (ImageFile != null && ImageFile.Length > 0)
                        {
                            company.PhotoUrl = UploadImage("CompanyLogo", ImageFile);
                        }

                        company.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        var result = _companyInfoData.CompanyInfoInsertUpdate(company, "Insert");
                        return Json(result.ID);
                    }
                    else
                    {
                        company.ID = viewModel.CompanyInfo.ID;
                        company.CompanyName = viewModel.CompanyInfo.CompanyName;
                        company.CompanyCode = viewModel.CompanyInfo.CompanyCode;
                        company.Description = viewModel.CompanyInfo.Description;
                        company.Address = viewModel.CompanyInfo.Address;
                        company.Email = viewModel.CompanyInfo.Email;
                        company.Phone = viewModel.CompanyInfo.Phone;
                        company.FacebookUrl = viewModel.CompanyInfo.FacebookUrl;
                        company.TwitterUrl = viewModel.CompanyInfo.TwitterUrl;
                        company.LinkedInUrl = viewModel.CompanyInfo.LinkedInUrl;
                        company.YouTubeUrl = viewModel.CompanyInfo.YouTubeUrl;
                        company.Website = viewModel.CompanyInfo.Website;
                        company.IsActive = true;

                        if (ImageFile != null && ImageFile.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.CompanyInfo.PhotoUrl))
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", viewModel.CompanyInfo.PhotoUrl);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }
                            company.PhotoUrl = UploadImage("CompanyLogo", ImageFile);
                        }
                        else
                        {
                            company.PhotoUrl = viewModel.CompanyInfo.PhotoUrl;
                        }

                        company.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        var result = _companyInfoData.CompanyInfoInsertUpdate(company, "Update");
                        return Json(result.ID);
                    }
                }
            }
            catch (Exception ex) { }
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
            imageName = $"../Admin/img/home/{fileName}";
            return imageName;
        }
    }
}
