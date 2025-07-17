using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly CustomerData _customerData;
        private readonly LocationTreeData _locationTreeData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CustomerController(IWebHostEnvironment webHostEnvironment)
        {
            _customerData = new CustomerData();
            _locationTreeData = new LocationTreeData();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin")]
        [SessionAuthorize]
        public IActionResult CustomerCreate()
        {
            return View();
        }
        [HttpGet]
        [SessionAuthorize]
        public ActionResult GetCustomerHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetCountryList();
                if (ID != 0)
                {
                    viewModel.Customer = _customerData.GetCustomer("", ID);
                    if (viewModel.Customer.ID != 0)
                    {
                        GetDistrictByCountryId(viewModel.Customer.CountryId);
                        GetPoliceStationByDistrictId(viewModel.Customer.DistrictId);
                        return PartialView("_GetCustomerHdrPView", viewModel);
                    }
                }
                else
                {
                    GetDistrictByCountryId(0);
                    GetPoliceStationByDistrictId(0);
                    viewModel.Customer = new CustomerMDL();
                    //viewModel.Customer.IsActive = true;
                    return PartialView("_GetCustomerHdrPView", viewModel);
                }
            }
            catch (Exception) { }

            return PartialView("_GetCustomerHdrPView", viewModel);
        }
        [HttpGet]
        [SessionAuthorize]
        public ActionResult GetCustomerDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.CustomerList = _customerData.GetCustomerList();
            }
            catch (Exception) { }

            return PartialView("_GetCustomerDetPView", viewModel);
        }
        [HttpPost]
        [SessionAuthorize]
        public IActionResult CustomerSetUpdate(AdminViewModel viewModel, IFormFile ImageFile)
        {
            try
            {
                if (viewModel != null)
                {
                    CustomerMDL customer = new CustomerMDL();
                    if (viewModel.Customer.ID == 0)
                    {
                        customer = _customerData.GetCustomer(viewModel.Customer.Email, 0);
                        if (customer.ID <= 0)
                        {
                            if (viewModel.Customer.Password == viewModel.Customer.ConfirmPassword)
                            {
                                customer.Name = viewModel.Customer.Name;
                                customer.Email = viewModel.Customer.Email;
                                customer.Phone = viewModel.Customer.Phone;
                                customer.CountryId = viewModel.Customer.CountryId;
                                customer.DistrictId = viewModel.Customer.DistrictId;
                                customer.PoliceStationId = viewModel.Customer.PoliceStationId;
                                customer.Address = viewModel.Customer.Address;
                                customer.Password = viewModel.Customer.Password;
                                if (ImageFile != null && ImageFile.Length > 0)
                                {
                                    customer.PhotoUrl = UploadImage(customer.Name.ToString(), ImageFile);
                                }
                                customer.IsActive = viewModel.Customer.IsActive;
                                customer.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                                var result = _customerData.CustomerSetUpdate(customer, "Insert");
                                return Json(result.ID);
                            }
                            else
                            {
                                return Json(-2);
                            }
                        }
                        else
                        {
                            return Json(-1);
                        }

                    }
                    else
                    {
                        if (viewModel.Customer.Password == viewModel.Customer.ConfirmPassword)
                        {
                            customer.ID = viewModel.Customer.ID;
                            customer.Name = viewModel.Customer.Name;
                            customer.Email = viewModel.Customer.Email;
                            customer.Phone = viewModel.Customer.Phone;
                            customer.CountryId = viewModel.Customer.CountryId;
                            customer.DistrictId = viewModel.Customer.DistrictId;
                            customer.PoliceStationId = viewModel.Customer.PoliceStationId;
                            customer.Address = viewModel.Customer.Address;
                            customer.Password = viewModel.Customer.Password;
                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(viewModel.Customer.PhotoUrl))
                                {
                                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", viewModel.Customer.PhotoUrl);
                                    if (System.IO.File.Exists(imagePath))
                                    {
                                        System.IO.File.Delete(imagePath);
                                    }

                                }
                                customer.PhotoUrl = UploadImage(customer.Name.ToString(), ImageFile);
                            }
                            else
                            {
                                customer.PhotoUrl = viewModel.Customer.PhotoUrl;
                            }
                            customer.IsActive = viewModel.Customer.IsActive;
                            customer.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                            var result = _customerData.CustomerSetUpdate(customer, "Update");
                            return Json(result.ID);
                        }
                        else
                        {
                            return Json(-2);
                        }
                    }
                }
            }
            catch (Exception) { }
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
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/img/customer");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            imageName = $"../Admin/img/customer/{fileName}";
            return imageName;
        }
        #region DropDown-------------------------------------------------
        [HttpGet]
        public void GetCountryList()
        {
            var country = _locationTreeData.GetLocationTreeList("Country", null);
            var countryList = country.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name,
            }).ToList();
            ViewBag.CountryList = countryList;
        }
        [HttpGet]
        public JsonResult GetDistrictByCountryId(int? ID)
        {
            var district = _locationTreeData.GetLocationTreeList("District", ID);
            var districtList = district.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            //=====================================================//
            if (ID == null)
            {
                ViewBag.DistrictList = new SelectList(districtList, "Value", "Text");
            }
            else
            {
                ViewBag.DistrictList = new SelectList(districtList, "Value", "Text");
            }
            return Json(districtList);
        }
        [HttpGet]
        public JsonResult GetPoliceStationByDistrictId(int? ID)
        {
            var policeStation = _locationTreeData.GetLocationTreeList("PoliceStation", ID);
            var policeStationList = policeStation.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            //=====================================================//
            if (ID == null)
            {
                ViewBag.PoliceStationList = new SelectList(policeStationList, "Value", "Text");
            }
            else
            {
                ViewBag.PoliceStationList = new SelectList(policeStationList, "Value", "Text");
            }
            return Json(policeStationList);
        }
        #endregion    
    }
}
