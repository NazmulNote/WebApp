using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class VendorController : Controller
    {
        private readonly LocationTreeData _locationTreeData;
        private readonly VendorData _vendorData;
        public VendorController()
        {
            _locationTreeData = new LocationTreeData();
            _vendorData = new VendorData();
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult VendorCreate()
        {
            return View();
        }
        public IActionResult GetVendorHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetCountryList();
                if (ID != 0)
                {
                    viewModel.Vendor = _vendorData.GetVendor(ID);
                    if (viewModel.Vendor.ID != 0)
                    {
                        GetDistrictByCountryId(viewModel.Vendor.CountryId);
                        GetPoliceStationByDistrictId(viewModel.Vendor.DistrictId);
                        return PartialView("_GetVendorHdrPView", viewModel);
                    }
                }
                else
                {
                    GetDistrictByCountryId(0);
                    GetPoliceStationByDistrictId(0);
                    viewModel.Vendor = new VendorMDL();
                    viewModel.Vendor.IsActive = true;
                    return PartialView("_GetVendorHdrPView", viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
            return PartialView("_GetVendorHdrPView", viewModel);
        }
        public IActionResult GetVendorDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.VendorList = _vendorData.GetVendorList();
            }
            catch (Exception ex) { }
            return PartialView("_GetVendorDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult VendorSetUpdate(AdminViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.Vendor != null)
                {
                    VendorMDL vendor = new VendorMDL();
                    VendorMDL existingVendor = _vendorData.CheckVendor(viewModel.Vendor.Name, viewModel.Vendor.Phone);

                    if (viewModel.Vendor.ID == 0)
                    {
                        // Insert
                        if (existingVendor == null || existingVendor.ID <= 0)
                        {
                            vendor.Name = viewModel.Vendor.Name;
                            vendor.ContactPerson = viewModel.Vendor.ContactPerson;
                            vendor.Phone = viewModel.Vendor.Phone;
                            vendor.Email = viewModel.Vendor.Email;
                            vendor.CountryId = viewModel.Vendor.CountryId;
                            vendor.DistrictId = viewModel.Vendor.DistrictId;
                            vendor.PoliceStationId = viewModel.Vendor.PoliceStationId;
                            vendor.Address = viewModel.Vendor.Address;
                            vendor.TIN = viewModel.Vendor.TIN;
                            vendor.IsActive = viewModel.Vendor.IsActive;
                            vendor.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                            var result = _vendorData.VendorInsertUpdate(vendor, "Insert");
                            return Json(result.ID);
                        }
                        else
                        {
                            return Json(-1); // Duplicate found
                        }
                    }
                    else
                    {
                        // Update
                        if (viewModel.Vendor.ID == existingVendor.ID || existingVendor.ID == 0)
                        {
                            vendor.ID = viewModel.Vendor.ID;
                            vendor.Name = viewModel.Vendor.Name;
                            vendor.ContactPerson = viewModel.Vendor.ContactPerson;
                            vendor.Phone = viewModel.Vendor.Phone;
                            vendor.Email = viewModel.Vendor.Email;
                            vendor.CountryId = viewModel.Vendor.CountryId;
                            vendor.DistrictId = viewModel.Vendor.DistrictId;
                            vendor.PoliceStationId = viewModel.Vendor.PoliceStationId;
                            vendor.Address = viewModel.Vendor.Address;
                            vendor.TIN = viewModel.Vendor.TIN;
                            vendor.IsActive = viewModel.Vendor.IsActive;
                            vendor.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                            var result = _vendorData.VendorInsertUpdate(vendor, "Update");
                            return Json(result.ID);
                        }
                        else
                        {
                            return Json(-1); // Duplicate found on update
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

            return Json(0);
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
