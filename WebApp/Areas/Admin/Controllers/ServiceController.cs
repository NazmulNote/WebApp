using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class ServiceController : Controller
    {
        private readonly ServiceData _serviceData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ServiceController(IWebHostEnvironment webHostEnvironment)
        {
            _serviceData = new ServiceData();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult ServiceCreate()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetServiceHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetServiceCatList();
                if (ID != 0)
                {
                    viewModel.Service = _serviceData.GetService(ID);
                    if (viewModel.Service.ID != 0)
                    {
                        return PartialView("_GetServiceHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.Service = new ServiceMDL();
                    viewModel.Service.IsActive = true;
                    return PartialView("_GetServiceHdrPView", viewModel);
                }
            }
            catch (Exception) { }

            return PartialView("_GetServiceHdrPView", viewModel);
        }
        [HttpGet]
        public ActionResult GetServiceDetPView(int? ServiceCatId)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.ServiceList = _serviceData.GetServiceList(ServiceCatId);
            }
            catch (Exception) { }

            return PartialView("_GetServiceDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult ServiceSetUpdate(AdminViewModel viewModel, IFormFile ImageFile)
        {
            try
            {
                if (viewModel != null)
                {
                    ServiceMDL service = new ServiceMDL();

                    if (viewModel.Service.ID == 0)
                    {
                        ServiceMDL existService = new ServiceMDL();
                        existService = _serviceData.ServiceCheck(viewModel.Service.ServiceCatId, viewModel.Service.Name);

                        if (existService.ID <= 0)
                        {
                            service.ServiceCatId = viewModel.Service.ServiceCatId;
                            service.Name = viewModel.Service.Name;
                            service.ShortDesc = viewModel.Service.ShortDesc;
                            service.FullDesc = viewModel.Service.FullDesc;
                            service.VideoUrl = viewModel.Service.VideoUrl;
                            service.IsActive = viewModel.Service.IsActive;

                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                service.PhotoUrl = UploadImage(service.Name.ToString(), ImageFile);
                            }

                            service.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                            service.InsertedByIP = HttpContext.Connection.RemoteIpAddress?.ToString();
                            service.CreatedAt = DateTime.Now;

                            var result = _serviceData.ServiceInsertUpdate(service, "Insert");
                            return Json(result.ID);
                        }
                        else
                        {
                            return Json(-1); // Already exists
                        }
                    }
                    else
                    {
                        service.ID = viewModel.Service.ID;
                        service.ServiceCatId = viewModel.Service.ServiceCatId;
                        service.Name = viewModel.Service.Name;
                        service.ShortDesc = viewModel.Service.ShortDesc;
                        service.FullDesc = viewModel.Service.FullDesc;
                        service.VideoUrl = viewModel.Service.VideoUrl;
                        service.IsActive = viewModel.Service.IsActive;

                        if (ImageFile != null && ImageFile.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.Service.PhotoUrl))
                            {
                                var photoPath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "../" + viewModel.Service.PhotoUrl);
                                if (System.IO.File.Exists(photoPath))
                                {
                                    System.IO.File.Delete(photoPath);
                                }
                            }

                            service.PhotoUrl = UploadImage(service.Name.ToString(), ImageFile);
                        }
                        else
                        {
                            service.PhotoUrl = viewModel.Service.PhotoUrl;
                        }

                        service.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        service.UpdatedAt = DateTime.Now;

                        var result = _serviceData.ServiceInsertUpdate(service, "Update");
                        return Json(result.ID);
                    }
                }
            }
            catch (Exception) { }

            return Json(0);
        }
        [HttpPost]
        public IActionResult ServiceDelete(int ID)
        {
            try
            {
                ServiceMDL Service = new ServiceMDL();
                Service = _serviceData.GetService(ID);
                if (Service.ID != 0)
                {
                    bool isDeleted = _serviceData.ServiceDelete(ID);
                    if (!string.IsNullOrEmpty(Service.PhotoUrl))
                    {
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "../" + Service.PhotoUrl);
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
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/img/service");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            imageName = $"/Admin/img/service/{fileName}";
            return imageName;
        }
        [HttpGet]
        public void GetServiceCatList()
        {
            var serviceCat = _serviceData.ServiceCat();
            var serviceCatList = serviceCat.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name,
            }).ToList();
            ViewBag.ServiceCatList = serviceCatList;
        }
    }
}
