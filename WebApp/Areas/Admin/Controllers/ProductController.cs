using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class ProductController : Controller
    {
        private readonly ProductData _productData;
        private readonly LibraryData _libraryData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _productData = new ProductData();
            _libraryData = new LibraryData();
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult ProductCreate()
        {
            return View();
        }
        public IActionResult GetProductHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetCategoryList();
                if (ID != 0)
                {
                    viewModel.Product = _productData.GetProduct(ID);
                    if(viewModel.Product.ID != 0)
                    {
                        GetSubCatByCatId(viewModel.Product.CatId);
                        GetSubChildCatBySubCatId(viewModel.Product.SubCatId);
                        return PartialView("_GetProductHdrPView", viewModel);
                    }
                }
                else
                {
                    GetSubCatByCatId(0);
                    GetSubChildCatBySubCatId(0);
                    viewModel.Product = new ProductMDL();
                    viewModel.Product.IsActive = true;
                    return PartialView("_GetProductHdrPView", viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
            return PartialView("_GetProductHdrPView", viewModel);
        }
        public IActionResult GetProductDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.ProductList = _productData.GetProductList();
            }
            catch (Exception ex) { }
            return PartialView("_GetProductDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult ProductSetUpdate(AdminViewModel viewModel,IFormFile ImageFile)
        {
            try
            {
                var desc = viewModel.Product.Description;
                if (viewModel != null)
                {
                    ProductMDL product = new ProductMDL();
                    ProductMDL existProduct = new ProductMDL();
                    existProduct = _productData.CheckProduct(viewModel.Product?.Name,viewModel.Product?.CatId, viewModel.Product?.SubCatId, viewModel.Product?.SubChildCatId);
                    if (viewModel.Product?.ID == 0)
                    {

                        if (existProduct.ID <= 0)
                        {
                            product.CatId = viewModel.Product.CatId;
                            product.SubCatId = viewModel.Product.SubCatId;
                            product.SubChildCatId = viewModel.Product.SubChildCatId;
                            product.Name = viewModel.Product.Name;
                            product.Code = viewModel.Product.Code;
                            product.Description = viewModel.Product.Description;
                            product.Specification = viewModel.Product.Specification;
                            product.SellingPrice = viewModel.Product.SellingPrice;
                            product.OfferPercent = viewModel.Product.OfferPercent;
                            product.OfferPrice = viewModel.Product.OfferPrice;
                            product.IsActive = viewModel.Product.IsActive;

                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                product.PhotoUrl = UploadImage(product.Name.ToString(), ImageFile);
                            }
                            product.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                            var result = _productData.ProductInsertUpdate(product, "Insert");
                            return Json(result.ID);

                        }
                        else
                        {
                            return Json(-1);
                        }
                    }
                    else
                    {
                        if (viewModel.Product.ID == existProduct.ID || existProduct.ID == 0)
                        {
                            product.ID = viewModel.Product.ID;
                            product.CatId = viewModel.Product.CatId;
                            product.SubCatId = viewModel.Product.SubCatId;
                            product.SubChildCatId = viewModel.Product.SubChildCatId;
                            product.Name = viewModel.Product.Name;
                            product.Code = viewModel.Product.Code;
                            product.Description = viewModel.Product.Description;
                            product.Specification = viewModel.Product.Specification;
                            product.SellingPrice = viewModel.Product.SellingPrice;
                            product.OfferPercent = viewModel.Product.OfferPercent;
                            product.OfferPrice = viewModel.Product.OfferPrice;
                            product.IsActive = viewModel.Product.IsActive;
                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(viewModel.Product.PhotoUrl))
                                {
                                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", viewModel.Product.PhotoUrl);
                                    if (System.IO.File.Exists(imagePath))
                                    {
                                        System.IO.File.Delete(imagePath);
                                    }

                                }
                                product.PhotoUrl = UploadImage(product.Name.ToString(), ImageFile);
                            }
                            else
                            {
                                product.PhotoUrl = viewModel.Product.PhotoUrl;
                            }
                            product.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                            var result = _productData.ProductInsertUpdate(product, "Update");
                            return Json(result.ID);
                        }
                        else
                        {
                            return Json(-1);
                        }

                    }

                }
            }
            catch (Exception ex) { }
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
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/img/product");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            imageName = $"../Admin/img/product/{fileName}";
            return imageName;
        }

        #region DropDown----------------------------------------------------------
        [HttpGet]
        public void GetCategoryList()
        {
            var category = _libraryData.GetLibraryList("Cat", null, null);
            var categoryList = category.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.ItemName,
            }).ToList();
            ViewBag.CategoryList = categoryList;
        }
        [HttpGet]
        public JsonResult GetSubCatByCatId(int? ID)
        {
            var subCat = _libraryData.GetLibraryList("SubCat", ID, null);
            var subCatList = subCat.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.ItemName
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
            var subChildCat = _libraryData.GetLibraryList("SubChildCat", null, ID);
            var subChildCatList = subChildCat.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.ItemName
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
        #endregion
    }
}
