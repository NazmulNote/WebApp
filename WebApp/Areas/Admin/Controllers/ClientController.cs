using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class ClientController : Controller
    {
        private readonly ClientData _clientData;
        private readonly LocationTreeData _locationTreeData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ClientController(IWebHostEnvironment webHostEnvironment)
        {
            _clientData = new ClientData();
            _locationTreeData = new LocationTreeData();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin")]
        public IActionResult ClientCreate()
        {
            return View();
        }
        public ActionResult GetClientHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetCountryList();
                if (ID != 0)
                {
                    viewModel.Client = _clientData.GetClient("", ID);
                    if (viewModel.Client.ID != 0)
                    {
                        GetDistrictByCountryId(viewModel.Client.CountryId);
                        GetPoliceStationByDistrictId(viewModel.Client.DistrictId);
                        return PartialView("_GetClientHdrPView", viewModel);
                    }
                }
                else
                {
                    GetDistrictByCountryId(0);
                    GetPoliceStationByDistrictId(0);
                    viewModel.Client = new ClientMDL();
                    viewModel.Client.IsActive = true;
                    return PartialView("_GetClientHdrPView", viewModel);
                }
            }
            catch (Exception) { }

            return PartialView("_GetClientHdrPView", viewModel);
        }
        public ActionResult GetClientDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.ClientList = _clientData.GetClientList();
            }
            catch (Exception) { }

            return PartialView("_GetClientDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult ClientSetUpdate(AdminViewModel viewModel, IFormFile ImageFile)
        {
            try
            {
                if (viewModel != null)
                {
                    ClientMDL client = new ClientMDL();

                    if (viewModel.Client.ID == 0)
                    {
                        ClientMDL existClient = _clientData.GetClient(viewModel.Client.Name, 0);
                        if (existClient.ID <= 0)
                        {
                            client.Name = viewModel.Client.Name;
                            client.ContactPerson = viewModel.Client.ContactPerson;
                            client.Email = viewModel.Client.Email;
                            client.Phone = viewModel.Client.Phone;
                            client.CountryId = viewModel.Client.CountryId;
                            client.DistrictId = viewModel.Client.DistrictId;
                            client.PoliceStationId = viewModel.Client.PoliceStationId;
                            client.Address = viewModel.Client.Address;
                            client.Website = viewModel.Client.Website;
                            client.ClientType = viewModel.Client.ClientType;

                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                client.PhotoUrl = UploadImage(client.Name ?? "", ImageFile);
                            }

                            client.IsActive = viewModel.Client.IsActive;
                            client.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                            var result = _clientData.ClientSetUpdate(client, "Insert");
                            return Json(result.ID);
                        }
                        else
                        {
                            return Json(-1);
                        }
                    }
                    else
                    {
                        client.ID = viewModel.Client.ID;
                        client.Name = viewModel.Client.Name;
                        client.ContactPerson = viewModel.Client.ContactPerson;
                        client.Email = viewModel.Client.Email;
                        client.Phone = viewModel.Client.Phone;
                        client.CountryId = viewModel.Client.CountryId;
                        client.DistrictId = viewModel.Client.DistrictId;
                        client.PoliceStationId = viewModel.Client.PoliceStationId;
                        client.Address = viewModel.Client.Address;
                        client.Website = viewModel.Client.Website;
                        client.ClientType = viewModel.Client.ClientType;

                        if (ImageFile != null && ImageFile.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.Client.PhotoUrl))
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "../" + "../" + viewModel.Client.PhotoUrl);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }
                            client.PhotoUrl = UploadImage(client.Name ?? "", ImageFile);
                        }
                        else
                        {
                            client.PhotoUrl = viewModel.Client.PhotoUrl;
                        }

                        client.IsActive = viewModel.Client.IsActive;
                        client.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                        var result = _clientData.ClientSetUpdate(client, "Update");
                        return Json(result.ID);
                    }
                }
            }
            catch (Exception){}

            return Json(0);
        }
        [HttpPost]
        public IActionResult ClientDelete(int ID)
        {
            try
            {
                ClientMDL client = _clientData.GetClient(null, ID); 
                if (client != null && client.ID != 0)
                {
                    bool isDeleted = _clientData.ClientDelete(ID);
                    if (!string.IsNullOrEmpty(client.PhotoUrl))
                    {
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", "../" + "../" + client.PhotoUrl);
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
            catch (Exception)
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
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/img/client");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            imageName = $"/Admin/img/client/{fileName}";
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
