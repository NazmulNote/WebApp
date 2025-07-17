using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class StockController : Controller
    {
        private readonly StockData _stockData;
        public StockController()
        {
            _stockData = new StockData();
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin", "AdminUser")]
        public IActionResult ProductStock()
        {
            return View();
        }
        public IActionResult GetProductStockHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetCategoryList();
                GetSubCatByCatId(0);
                GetSubChildCatBySubCatId(0);
                GetProductBySubChildCatId(0);
                viewModel.ProductStock = new ProductStockMDL();
                viewModel.ProductStock.FromDate = DateTime.Today;
                viewModel.ProductStock.ToDate = DateTime.Today;
                return PartialView("_GetProductStockHdrPView", viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
        }
        public IActionResult GetProductStockDetPView(DateTime? FromDate,DateTime? ToDate = null,int ? CatId = null, int? SubCatId = null, int? SubChildCatId = null, int? ProductId = null)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.ProductStockList = _stockData.GetProductStockList(FromDate,ToDate,CatId, SubCatId,SubChildCatId,ProductId);
            }
            catch (Exception ex) { }
            return PartialView("_GetProductStockDetPView", viewModel);
        }
        #region DropDown-------------------------------------------------------
        [HttpGet]
        public void GetCategoryList()
        {
            var category = _stockData.GetProductStockDropDownList("Cat", null, null,null);
            var categoryList = category.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name,
            }).ToList();
            ViewBag.CategoryList = categoryList;
        }
        [HttpGet]
        public JsonResult GetSubCatByCatId(int? ID)
        {
            var subCat = _stockData.GetProductStockDropDownList("SubCat", ID, null,null);
            var subCatList = subCat.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            //=====================================================//
            if (ID == null)
            {
                ViewBag.SubCatList = new SelectList(subCatList, "Value", "Text");
            }
            else
            {
                ViewBag.SubCatList = new SelectList(subCatList, "Value", "Text");
            }
            return Json(subCatList);
        }
        [HttpGet]
        public JsonResult GetSubChildCatBySubCatId(int? ID)
        {
            var subChildCat = _stockData.GetProductStockDropDownList("SubChildCat", null, ID,null);
            var subChildCatList = subChildCat.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            //=====================================================//
            if (ID == null)
            {
                ViewBag.subChildCatList = new SelectList(subChildCatList, "Value", "Text");
            }
            else
            {
                ViewBag.subChildCatList = new SelectList(subChildCatList, "Value", "Text");
            }
            return Json(subChildCatList);
        }
        [HttpGet]
        public JsonResult GetProductBySubChildCatId(int? ID)
        {
            var product = _stockData.GetProductStockDropDownList("Product", null, null, ID);
            var productList = product.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name
            }).ToList();
            //=====================================================//
            if (ID == null)
            {
                ViewBag.ProductList = new SelectList(productList, "Value", "Text");
            }
            else
            {
                ViewBag.ProductList = new SelectList(productList, "Value", "Text");
            }
            return Json(productList);
        }
        #endregion
    }
}
