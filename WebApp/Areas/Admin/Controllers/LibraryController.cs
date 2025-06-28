using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class LibraryController : Controller
    {
        private readonly LibraryData _libraryData;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LibraryController(IWebHostEnvironment webHostEnvironment)
        {
            _libraryData = new LibraryData();
            _webHostEnvironment = webHostEnvironment;
        }
        #region Category---------------------------------------------------------
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult GetCatHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                if (ID != 0)
                {
                    viewModel.Library = _libraryData.GetLibrary("Cat", ID);
                    if (viewModel.Library.ID != 0)
                    {
                        return PartialView("_GetCatHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.Library = new LibraryMDL();
                    viewModel.Library.Item = "Cat";
                    return PartialView("_GetCatHdrPView", viewModel);
                }
            }
            catch (Exception ex) { }
            return PartialView("_GetCatHdrPView", viewModel);
        }
        public IActionResult GetCatDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.LibraryList = _libraryData.GetLibraryList("Cat", null, null);
            }
            catch (Exception ex) { }
            return PartialView("_GetCatDetPView", viewModel);
        }
        #endregion
        #region SubCategory------------------------------------------------------
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult SubCat()
        {
            return View();
        }
        public IActionResult GetSubCatHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetCategoryList();
                if (ID != 0)
                {
                    viewModel.Library = _libraryData.GetLibrary("SubCat", ID);
                    if (viewModel.Library.ID != 0)
                    {
                        return PartialView("_GetSubCatHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.Library = new LibraryMDL();
                    viewModel.Library.Item = "SubCat";
                    return PartialView("_GetSubCatHdrPView", viewModel);
                }
            }
            catch (Exception ex) { }
            return PartialView("_GetSubCatHdrPView", viewModel);
        }
        public IActionResult GetSubCatDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.LibraryList = _libraryData.GetLibraryList("SubCat",null, null);
            }
            catch (Exception ex) { }
            return PartialView("_GetSubCatDetPView", viewModel);
        }
        #endregion
        #region SubChildCat------------------------------------------------------
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult SubChildCat()
        {
            return View();
        }
        public IActionResult GetSubChildCatHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                GetCategoryList();
                
                if (ID != 0)
                {
                    
                    viewModel.Library = _libraryData.GetLibrary("SubChildCat", ID);
                    if (viewModel.Library.ID != 0)
                    {
                        GetSubCatByCatId(viewModel.Library.CategoryId);
                        return PartialView("_GetSubChildCatHdrPView", viewModel);
                    }
                }
                else
                {
                    GetSubCatByCatId(0);
                    viewModel.Library = new LibraryMDL();
                    viewModel.Library.Item = "SubChildCat";
                    return PartialView("_GetSubChildCatHdrPView", viewModel);
                }
            }
            catch (Exception ex) { }
            return PartialView("_GetSubChildCatHdrPView", viewModel);
        }
        public IActionResult GetSubChildCatDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.LibraryList = _libraryData.GetLibraryList("SubChildCat", null, null);
            }
            catch (Exception ex) { }
            return PartialView("_GetSubChildCatDetPView", viewModel);
        }
        #endregion----------------------------------------------------------------
        #region Service Category--------------------------------------------------
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult ServiceCat()
        {
            return View();
        }
        public IActionResult GetServiceCatHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                if (ID != 0)
                {
                    viewModel.Library = _libraryData.GetLibrary("ServiceCat", ID);
                    if (viewModel.Library.ID != 0)
                    {
                        return PartialView("_GetServiceCatHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.Library = new LibraryMDL();
                    viewModel.Library.Item = "ServiceCat";
                    return PartialView("_GetServiceCatHdrPView", viewModel);
                }
            }
            catch (Exception ex) { }
            return PartialView("_GetServiceCatHdrPView", viewModel);
        }
        public IActionResult GetServiceCatDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.LibraryList = _libraryData.GetLibraryList("ServiceCat", null, null);
            }
            catch (Exception ex) { }
            return PartialView("_GetServiceCatDetPView", viewModel);
        }
        #endregion ---------------------------------------------------------------
        public IActionResult LibrarySetUpdate(AdminViewModel viewModel,IFormFile ImageFile)
        {
            try
            {
                if(viewModel != null)
                {
                    LibraryMDL library = new LibraryMDL();
                    LibraryMDL existLibrary = new LibraryMDL();
                    existLibrary = _libraryData.CheckLibrary(viewModel.Library.Item, viewModel.Library.CategoryId, viewModel.Library.SubCatId, viewModel.Library.ItemName);
                    if (viewModel.Library?.ID == 0)
                    {

                        if (existLibrary.ID <= 0)
                        {
                            library.Item = viewModel.Library.Item;
                            library.CategoryId = viewModel.Library.CategoryId;
                            library.SubCatId = viewModel.Library.SubCatId;
                            library.ItemName = viewModel.Library.ItemName;
                            library.ItemCode = viewModel.Library.ItemCode;
                            library.Description = viewModel.Library.Description;
                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                library.PhotoUrl = UploadImage(library.Item.ToString(), ImageFile);
                            }
                            library.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                            var result = _libraryData.LibraryInsertUpdate(library, "Insert");
                            return Json(result.ID);

                        }
                        else
                        {
                            return Json(-1);
                        }
                    }
                    else
                    {
                        if(viewModel.Library.ID == existLibrary.ID || existLibrary.ID ==0)
                        {
                            library.ID = viewModel.Library.ID;
                            library.Item = viewModel.Library.Item;
                            library.CategoryId = viewModel.Library.CategoryId;
                            library.SubCatId = viewModel.Library.SubCatId;
                            library.ItemName = viewModel.Library.ItemName;
                            library.ItemCode = viewModel.Library.ItemCode;
                            library.Description = viewModel.Library.Description;
                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(viewModel.Library.PhotoUrl))
                                {
                                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", viewModel.Library.PhotoUrl);
                                    if (System.IO.File.Exists(imagePath))
                                    {
                                        System.IO.File.Delete(imagePath);
                                    }

                                }
                                library.PhotoUrl = UploadImage(library.Item.ToString(), ImageFile);
                            }
                            else
                            {
                                library.PhotoUrl = viewModel.Library.PhotoUrl;
                            }
                            library.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                            var result = _libraryData.LibraryInsertUpdate(library, "Update");
                            return Json(result.ID);
                        }
                        else
                        {
                            return Json(-1);
                        }
                        
                    }

                }
            }catch (Exception ex) { }
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
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/img/library");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            imageName = $"../Admin/img/library/{fileName}";
            return imageName;
        }
        #region Dropdown-------------------------------------------
        [HttpGet]
        public void GetCategoryList()
        {
            var category = _libraryData.GetLibraryList("Cat",null,null);
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
            var subCat = _libraryData.GetLibraryList("SubCat",ID, null);
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

        #endregion
    }
}
