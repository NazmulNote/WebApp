using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Models;
using WebApp.Areas.Client.Data;
using WebApp.Areas.Client.Models;

namespace WebApp.Areas.Client.Controllers
{
    [Area("Client")]
    public class CstmrController : Controller
    {
        private readonly CustomerData _customerData;
        private readonly HomeData _homeData;
        private readonly OrderProductData _orderProductData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CstmrController(IWebHostEnvironment webHostEnvironment)
        {
            _customerData = new CustomerData();
            _homeData = new HomeData();
            _orderProductData = new OrderProductData();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                var refererUrl = Request.Headers["Referer"].ToString();
                if(!string.IsNullOrEmpty(refererUrl) &&
                    !refererUrl.Contains("/Customer/SignUp", StringComparison.OrdinalIgnoreCase))
                {
                    HttpContext.Session.SetString("RefererUrl", refererUrl);
                }
                viewModel.CompanyInfo = _homeData.GetCompanyInfo();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SignIn(ClientViewModel viewModel)
        {
            ClientViewModel customerViewMdl = new ClientViewModel();
            customerViewMdl.CompanyInfo = _homeData.GetCompanyInfo();
            try
            {
                if (viewModel!=null)
                {
                    CustomerMDL customer = new CustomerMDL();
                    CustomerMDL existCustomer = _customerData.GetCustomer(viewModel.Customer.Email, null);
                    if (existCustomer.ID !=0)
                    {
                        customerViewMdl.Customer = _customerData.CustomerLogin(viewModel.Customer.Email, viewModel.Customer.Password);
                        if(customerViewMdl.Customer.ID != 0)
                        {
                            HttpContext.Session.SetString("CustomerID", customerViewMdl.Customer.ID.ToString());
                            HttpContext.Session.SetString("CustomerEmail", customerViewMdl.Customer.Email.ToString());
                            HttpContext.Session.SetString("CustomerName", customerViewMdl.Customer.Name.ToString());
                            HttpContext.Session.SetString("CustomerPhoto", customerViewMdl.Customer.PhotoUrl.ToString());
                            var refererUrl = HttpContext.Session.GetString("RefererUrl");
                            if (!string.IsNullOrEmpty(refererUrl))
                            {
                                return Redirect(refererUrl);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.error = "Incorrect password. Please try again.";
                            return View(customerViewMdl);
                        }
                    }
                    else
                    {
                        ViewBag.error = "User not found. Please try another email.";
                        return View(customerViewMdl);
                    }
                }
            }catch(Exception ex) { ViewBag.error = "Something went wrong."; }
            return View(customerViewMdl);
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                GetCountryList();
                GetDistrictByCountryId(0);
                GetPoliceStationByDistrictId(0);
                viewModel.CompanyInfo = _homeData.GetCompanyInfo();
            }
            catch (Exception ex) { }
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SignUp(ClientViewModel viewModel)
        {
            ClientViewModel customerViewMdl = new ClientViewModel();
            customerViewMdl.CompanyInfo = _homeData.GetCompanyInfo();
            try
            {
                GetCountryList();
                GetDistrictByCountryId(viewModel.Customer.CountryId);
                GetPoliceStationByDistrictId(viewModel.Customer.DistrictId);
                if (viewModel != null)
                {
                    CustomerMDL existCustomer = new CustomerMDL();
                    existCustomer = _customerData.GetCustomer(viewModel.Customer.Email, null);
                    if(existCustomer.ID <= 0)
                    {
                        if(viewModel.Customer.Password == viewModel.Customer.ConfirmPassword)
                        {
                            CustomerMDL customer = new CustomerMDL();
                            customer.Name = viewModel.Customer.Name;
                            customer.Email = viewModel.Customer.Email;
                            customer.Phone = viewModel.Customer.Phone;
                            customer.CountryId = viewModel.Customer.CountryId;
                            customer.DistrictId = viewModel.Customer.DistrictId;
                            customer.PoliceStationId = viewModel.Customer.PoliceStationId;
                            customer.Address = viewModel.Customer.Address;
                            if (viewModel.Customer.Photo != null && viewModel.Customer.Photo.Length > 0)
                            {
                                customer.PhotoUrl = UploadImage(customer.Name.ToString(), viewModel.Customer.Photo);
                            }
                             customer.IsActive = true;
                            customer.Password = viewModel.Customer.Password;
                            var result = _customerData.CustomerSetUpdate(customer, "Insert");
                        }
                        else
                        {
                            ViewBag.Error = "Passwords do not match!";
                            return View(customerViewMdl);
                        }
                    }
                    else
                    {
                        ViewBag.Error = "You already have an account with this email. Please log in or try another email.";
                        return View(customerViewMdl);
                    }
                }
            }
            catch (Exception ex) { }
            ViewBag.Success = "Sign Up completed successfully!";
            TempData["Success"] = "Sign Up completed successfully! Please Sign In";
            return RedirectToAction("SignIn");
        }
        [HttpGet]
        public IActionResult Profile()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                var customerId = HttpContext.Session.GetString("CustomerID");
                if(customerId != null)
                {
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("SignIn");
                }
            }
            catch(Exception ex) { }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult GetCustomerHdrPView()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                GetCountryList();
                var customerId = HttpContext.Session.GetString("CustomerID");
                if (customerId != null)
                {
                    viewModel.Customer = _customerData.GetCustomer("", Convert.ToInt32(customerId));
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
                    return PartialView("_GetCustomerHdrPView", viewModel);
                }
            }
            catch (Exception) { }

            return PartialView("_GetCustomerHdrPView", viewModel);
        }
        [HttpGet]
        public IActionResult GetCustomerDetPView()
        {
            ClientViewModel viewModel = new ClientViewModel();
            try
            {
                var customerId = HttpContext.Session.GetString("CustomerID");
                viewModel.OrderProductList = _orderProductData.GetOrderProductList(Convert.ToInt32(customerId));
                viewModel.CustomerList = new List<CustomerMDL>();
               
            }
            catch (Exception) { }

            return PartialView("_GetCustomerDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult CustomerUpdate(ClientViewModel viewModel, IFormFile ImageFile)
        {
            try
            {
                if (viewModel != null)
                {
                    CustomerMDL customer = new CustomerMDL();
                    CustomerMDL existCustomer = new CustomerMDL();
                    existCustomer = _customerData.GetCustomer(viewModel.Customer.Email, null);
                    if (viewModel.Customer?.ID !=0)
                    {
                        if (viewModel.Customer.ID == existCustomer.ID || existCustomer.ID ==0)
                        {
                            if (viewModel.Customer?.Password == viewModel.Customer?.ConfirmPassword)
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
                                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "../" + viewModel.Customer.PhotoUrl);
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
                        else
                        {
                            return Json(-1);
                        }
                    }
                }
            }
            catch (Exception) { }
            return Json(0);
        }
        public IActionResult SignOut()
        {
            HttpContext.Session.SetString("RefererUrl", Request.Headers["Referer"].ToString());
            var customerId = HttpContext.Session.GetString("CustomerID");
            var refererUrl = HttpContext.Session.GetString("RefererUrl");
            if (customerId != null)
            {
                HttpContext.Session.Clear();
            }

            if (!string.IsNullOrEmpty(refererUrl))
            {
                return Redirect(refererUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #region DropDown--------------------------------------------------
        [HttpGet]
        public void GetCountryList()
        {
            var country = _customerData.GetLocationTreeList("Country", null);
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
            var district = _customerData.GetLocationTreeList("District", ID);
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
            var policeStation = _customerData.GetLocationTreeList("PoliceStation", ID);
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
            imageName = $"/Admin/img/customer/{fileName}";
            return imageName;
        }
    }
}
