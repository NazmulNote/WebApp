using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class ProductPurchaseController : Controller
    {
        private readonly ProductData _productData;
        private readonly VendorData _vendorData;
        private readonly ProductPurchaseData _productPurchaseData;
        public ProductPurchaseController()
        {
            _productData = new ProductData();
            _vendorData = new VendorData();
            _productPurchaseData = new ProductPurchaseData();
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin","AdminUser")]
        public IActionResult ProductPurchaseCreate()
        {
            return View();
        }
        public IActionResult GetProductPurchaseHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetVendorList();
                GetProductList();
                if (ID != 0)
                {
                    viewModel.ProductPurchase = _productPurchaseData.GetProductPurchase(ID);
                    if (viewModel.ProductPurchase.ID != 0)
                    {
                        return PartialView("_GetProductPurchaseHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.ProductPurchase = new ProductPurchaseMDL();
                    viewModel.ProductPurchase.IsActive = true;
                    return PartialView("_GetProductPurchaseHdrPView", viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
            return PartialView("_GetProductPurchaseHdrPView", viewModel);
        }
        public IActionResult GetProductPurchaseDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.ProductPurchaseList = _productPurchaseData.GetProductPurchaseList();
            }
            catch (Exception ex) { }
            return PartialView("_GetProductPurchaseDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult ProductPurchaseSetUpdate(AdminViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.ProductPurchase != null)
                {
                    ProductPurchaseMDL productPurchase = new ProductPurchaseMDL();

                    // Optional Duplicate Check: based on ProductId, VendorId, InvoiceNo
                    var existingProductParchase = _productPurchaseData.CheckProductPurchase(
                        viewModel.ProductPurchase.ProductId,
                        viewModel.ProductPurchase.VendorId,
                        viewModel.ProductPurchase.InvoiceNo
                    );

                    if (viewModel.ProductPurchase.ID == 0)
                    {
                        // Insert
                        if (existingProductParchase == null || existingProductParchase.ID <= 0)
                        {
                            productPurchase.ProductId = viewModel.ProductPurchase.ProductId;
                            productPurchase.VendorId = viewModel.ProductPurchase.VendorId;
                            productPurchase.Qty = viewModel.ProductPurchase.Qty;
                            productPurchase.PurchasePrice = viewModel.ProductPurchase.PurchasePrice;
                            productPurchase.PurchaseDate = viewModel.ProductPurchase.PurchaseDate;
                            productPurchase.InvoiceNo = viewModel.ProductPurchase.InvoiceNo;
                            productPurchase.Remarks = viewModel.ProductPurchase.Remarks;
                            productPurchase.IsActive = viewModel.ProductPurchase.IsActive;
                            productPurchase.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                            var result = _productPurchaseData.ProductPurchaseInsertUpdate(productPurchase, "Insert");
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
                        if (viewModel.ProductPurchase.ID == existingProductParchase.ID || existingProductParchase.ID == 0)
                        {
                            productPurchase.ID = viewModel.ProductPurchase.ID;
                            productPurchase.ProductId = viewModel.ProductPurchase.ProductId;
                            productPurchase.VendorId = viewModel.ProductPurchase.VendorId;
                            productPurchase.Qty = viewModel.ProductPurchase.Qty;
                            productPurchase.PurchasePrice = viewModel.ProductPurchase.PurchasePrice;
                            productPurchase.PurchaseDate = viewModel.ProductPurchase.PurchaseDate;
                            productPurchase.InvoiceNo = viewModel.ProductPurchase.InvoiceNo;
                            productPurchase.Remarks = viewModel.ProductPurchase.Remarks;
                            productPurchase.IsActive = viewModel.ProductPurchase.IsActive;
                            productPurchase.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));

                            var result = _productPurchaseData.ProductPurchaseInsertUpdate(productPurchase, "Update");
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
        public void GetVendorList()
        {
            var vendor = _vendorData.GetVendorList();
            var vendorList = vendor.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name,
            }).ToList();
            ViewBag.VendorList = vendorList;
        }
        [HttpGet]
        public void GetProductList()
        {
            var product = _productData.GetProductList();
            var productList = product.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.Name,
            }).ToList();
            ViewBag.ProductList = productList;
        }
       
        #endregion    
    }
}
