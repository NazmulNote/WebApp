using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class SliderController : Controller
    {
        private readonly SliderData _sliderData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderController(IWebHostEnvironment webHostEnvironment)
        {
            _sliderData = new SliderData();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult SliderCreate()
        {
            return View();
        }
        public IActionResult GetSliderHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                if (ID != 0)
                {
                    viewModel.Slider = _sliderData.GetSlider(ID);
                    if (viewModel.Slider.ID != 0)
                    {
                        return PartialView("_GetSliderHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.Slider = new SliderMDL();
                    return PartialView("_GetSliderHdrPView", viewModel);
                }
            }
            catch (Exception ex) { }
            return PartialView("_GetSliderHdrPView", viewModel);
        }
        public IActionResult GetSliderDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.SliderList = _sliderData.GetSliderList();
            }
            catch (Exception ex) { }
            return PartialView("_GetSliderDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult SliderSetUpdate(AdminViewModel viewModel, IFormFile ImageFile)
        {
            try
            {
                if (viewModel != null)
                {
                    AdminViewModel sliderMDL = new AdminViewModel();
                    sliderMDL.SliderList = _sliderData.GetSliderList();
                    if(sliderMDL.SliderList.Count() < 10)
                    {

                        SliderMDL slider = new SliderMDL();
                        if (viewModel.Slider?.ID == 0)
                        {

                            slider.Title = viewModel.Slider.Title;
                            slider.Description = viewModel.Slider.Description;
                            slider.IsActive = viewModel.Slider.IsActive;
                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                slider.PhotoUrl = UploadImage("Slider", ImageFile);
                            }
                            slider.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                            var result = _sliderData.SliderInsertUpdate(slider, "Insert");
                            return Json(result.ID);
                        }
                        else
                        {
                            slider.ID = viewModel.Slider.ID;
                            slider.Title = viewModel.Slider.Title;
                            slider.Description = viewModel.Slider.Description;
                            slider.IsActive = viewModel.Slider.IsActive;
                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(viewModel.Slider.PhotoUrl))
                                {
                                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", viewModel.Slider.PhotoUrl);
                                    if (System.IO.File.Exists(imagePath))
                                    {
                                        System.IO.File.Delete(imagePath);
                                    }

                                }
                                slider.PhotoUrl = UploadImage("Slider", ImageFile);
                            }
                            else
                            {
                                slider.PhotoUrl = viewModel.Slider.PhotoUrl;
                            }
                            slider.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                            var result = _sliderData.SliderInsertUpdate(slider, "Update");
                            return Json(result.ID);

                        }
                    }
                }
            }
            catch (Exception ex) { }
            return Json(0);
        }
        [HttpPost]
        public IActionResult SliderDelete(int ID)
        {
            try
            {
                SliderMDL slider = new SliderMDL();
                slider = _sliderData.GetSlider(ID);
                if(slider.ID != 0)
                {
                    bool isDeleted = _sliderData.SliderDelete(ID);
                    if (!string.IsNullOrEmpty(slider.PhotoUrl))
                    {
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", slider.PhotoUrl);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                    }
                    return Json(isDeleted ? 1 : 0);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception ex)
            {
                return Json(0);
            }
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
