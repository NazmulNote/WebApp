using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class LocationTreeController : Controller
    {
        private readonly LocationTreeData _locationTreeData;
        public LocationTreeController()
        {
            _locationTreeData = new LocationTreeData();
        }
        #region Country-------------------------------------------
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult Country()
        {
            return View();
        }
        public IActionResult GetCountryHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                if (ID != 0)
                {
                    viewModel.LocationTree = _locationTreeData.GetLocationTree(ID);
                    if (viewModel.LocationTree.ID != 0)
                    {
                        return PartialView("_GetCountryHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.LocationTree = new LocationTreeMDL();
                    viewModel.LocationTree.Item = "Country";
                    viewModel.LocationTree.IsActive = true;
                    return PartialView("_GetCountryHdrPView", viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
            return PartialView("_GetCountryHdrPView", viewModel);
        }
        public IActionResult GetCountryDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.LocationTreeList = _locationTreeData.GetLocationTreeList("Country",null);
            }
            catch (Exception ex) { }
            return PartialView("_GetCountryDetPView", viewModel);
        }
        #endregion ------------------------------------------------
        #region District-------------------------------------------
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult District()
        {
            return View();
        }
        public IActionResult GetDistrictHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetCountryList();
                if (ID != 0)
                {
                    viewModel.LocationTree = _locationTreeData.GetLocationTree(ID);
                    if (viewModel.LocationTree.ID != 0)
                    {
                        return PartialView("_GetDistrictHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.LocationTree = new LocationTreeMDL();
                    viewModel.LocationTree.Item = "District";
                    viewModel.LocationTree.PId = 1;
                    viewModel.LocationTree.IsActive = true;
                    return PartialView("_GetDistrictHdrPView", viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
            return PartialView("_GetDistrictHdrPView", viewModel);
        }
        public IActionResult GetDistrictDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.LocationTreeList = _locationTreeData.GetLocationTreeList("District",null);
            }
            catch (Exception ex) { }
            return PartialView("_GetDistrictDetPView", viewModel);
        }
        #endregion ------------------------------------------------
        #region PoliceStation-------------------------------------------
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult PoliceStation()
        {
            return View();
        }
        public IActionResult GetPoliceStationHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetCountryList();
                if (ID != 0)
                {
                    viewModel.LocationTree = _locationTreeData.GetLocationTree(ID);
                    if (viewModel.LocationTree.ID != 0)
                    {
                        GetDistrictByCountryId(viewModel.LocationTree.CountryId);
                        return PartialView("_GetPoliceStationHdrPView", viewModel);
                    }
                }
                else
                {
                    GetDistrictByCountryId(0);
                    viewModel.LocationTree = new LocationTreeMDL();
                    viewModel.LocationTree.Item = "PoliceStation";
                    viewModel.LocationTree.PId = 1;
                    viewModel.LocationTree.IsActive = true;
                    return PartialView("_GetPoliceStationHdrPView", viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
            return PartialView("_GetPoliceStationHdrPView", viewModel);
        }
        public IActionResult GetPoliceStationDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.LocationTreeList = _locationTreeData.GetLocationTreeList("PoliceStation", null);
            }
            catch (Exception ex) { }
            return PartialView("_GetPoliceStationDetPView", viewModel);
        }
        #endregion ------------------------------------------------
        [HttpPost]
        public IActionResult LocationTreeSetUpdate(AdminViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    LocationTreeMDL locationTree = new LocationTreeMDL();
                    LocationTreeMDL existLocationTree = _locationTreeData.CheckLocationTree(viewModel.LocationTree.Name,viewModel.LocationTree.PId);

                    if (viewModel.LocationTree.ID == 0)
                    {
                        // Insert
                        if (existLocationTree == null || existLocationTree.ID <= 0)
                        {
                            locationTree.Item = viewModel.LocationTree.Item;
                            locationTree.Name = viewModel.LocationTree.Name;
                            locationTree.PId = viewModel.LocationTree.PId;
                            locationTree.Code = viewModel.LocationTree.Code;
                            locationTree.IsActive = viewModel.LocationTree.IsActive;
                            locationTree.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                            var result = _locationTreeData.LocationTreeInsertUpdate(locationTree, "Insert");
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
                        if (viewModel.LocationTree.ID == existLocationTree.ID || existLocationTree.ID == 0)
                        {
                            locationTree.ID = viewModel.LocationTree.ID;
                            locationTree.Item = viewModel.LocationTree.Item;
                            locationTree.Name = viewModel.LocationTree.Name;
                            locationTree.PId = viewModel.LocationTree.PId;
                            locationTree.Code = viewModel.LocationTree.Code;
                            locationTree.IsActive = viewModel.LocationTree.IsActive;
                            locationTree.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                            var result = _locationTreeData.LocationTreeInsertUpdate(locationTree, "Update");
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
        [HttpGet]
        public void GetCountryList()
        {
            var country = _locationTreeData.GetLocationTreeList("Country",null);
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
    }
}
