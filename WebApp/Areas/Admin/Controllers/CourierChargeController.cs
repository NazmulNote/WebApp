using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class CourierChargeController : Controller
    {
        private readonly CourierChargeData _courierChargeData;
        public CourierChargeController()
        {
            _courierChargeData = new CourierChargeData();
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult CourierChargeCreate()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCourierChargeHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                if (ID != 0)
                {
                    viewModel.CourierCharge = _courierChargeData.GetCourierCharge(ID);
                    if (viewModel.CourierCharge.ID != 0)
                    {
                        return PartialView("_GetCourierChargeHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.CourierCharge = new CourierChargeMDL();
                    viewModel.CourierCharge.IsActive = true;
                    return PartialView("_GetCourierChargeHdrPView", viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
            return PartialView("_GetCourierChargeHdrPView", viewModel);
        }
        [HttpGet]
        public ActionResult GetCourierChargeDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.CourierChargeList = _courierChargeData.GetCourierChargeList();
            }
            catch (Exception ex) { }
            return PartialView("_GetCourierChargeDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult CourierChargeSetUpdate(AdminViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.CourierCharge != null)
                {
                    CourierChargeMDL courierCharge = new CourierChargeMDL();
                    CourierChargeMDL existingCC = _courierChargeData.CheckCourierCharge(viewModel.CourierCharge.Location);

                    if (viewModel.CourierCharge.ID == 0)
                    {
                        // Insert
                        if (existingCC == null || existingCC.ID <= 0)
                        {
                            courierCharge.Location = viewModel.CourierCharge.Location;
                            courierCharge.ChargeAmount = viewModel.CourierCharge.ChargeAmount;
                            courierCharge.EstimatedDays = viewModel.CourierCharge.EstimatedDays;
                            courierCharge.IsActive = viewModel.CourierCharge.IsActive;
                            courierCharge.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                            var result = _courierChargeData.CourierChargeInsertUpdate(courierCharge, "Insert");
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
                        if (viewModel.CourierCharge.ID == existingCC.ID || existingCC.ID == 0)
                        {
                            courierCharge.ID = viewModel.CourierCharge.ID;
                            courierCharge.Location = viewModel.CourierCharge.Location;
                            courierCharge.ChargeAmount = viewModel.CourierCharge.ChargeAmount;
                            courierCharge.EstimatedDays = viewModel.CourierCharge.EstimatedDays;
                            courierCharge.IsActive = viewModel.CourierCharge.IsActive;
                            courierCharge.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                            var result = _courierChargeData.CourierChargeInsertUpdate(courierCharge, "Update");
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
    }
}
