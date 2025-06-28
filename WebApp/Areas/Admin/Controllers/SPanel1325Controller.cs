using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SPanel1325Controller : Controller
    {
        private readonly AdminUserData _adminUserData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SPanel1325Controller(IWebHostEnvironment webHostEnvironment)
        {
            _adminUserData = new AdminUserData();
            _webHostEnvironment = webHostEnvironment;
        }
        [SessionAuthorize]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("AUserId") == null || HttpContext.Session.GetString("AUserId") == "") { return RedirectToAction("Login", "SPanel1325"); }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AdminViewModel viewModel)
        {
            try
            {
                AdminUserMDL adminUserMDL = new AdminUserMDL();
                adminUserMDL = _adminUserData.AdminUserLogin(viewModel.AdminUser.Email,viewModel.AdminUser.Password);
                if(adminUserMDL.ID != 0)
                {
                    if (adminUserMDL.IsActive)
                    {
                        HttpContext.Session.SetString("AUserId", adminUserMDL.ID.ToString());
                        HttpContext.Session.SetString("AUserRole", adminUserMDL.Role.ToString());

                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.Error = "You are not an active user. Please contact your administrator.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "Your email or password is incorrect. Please use the correct information.";
                    return View();
                }

            }
            catch(Exception e)
            {
                throw new Exception("Error in login :" + e.Message);
            }
        }
        public IActionResult Logout()
        {
            try
            {
                var AUserId = HttpContext.Session.GetString("AUserId");
                if (AUserId != null)
                {

                    HttpContext.Session.Clear();
                }
            }
            catch (Exception) { }
            return RedirectToAction("Login", "SPanel1325");
        }
        [HttpGet]
        public IActionResult Registration()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Registration(AdminViewModel viewModel)
        {
            try
            {
                if (viewModel != null)
                {
                    AdminUserMDL existAdmin = new AdminUserMDL();
                    existAdmin = _adminUserData.GetAdminUser(viewModel.AdminUser.Email, 0);
                    if (existAdmin.ID <= 0)
                    {
                        if (viewModel.AdminUser.Password == viewModel.AdminUser.ConfirmPassword)
                        {
                            AdminUserMDL adminUser = new AdminUserMDL();
                            //string encryptedPassword = EncryptionHelper.Encrypt(viewModel.User.Password);
                            adminUser.Name = viewModel.AdminUser.Name;
                            adminUser.EmpID = viewModel.AdminUser.EmpID;
                            adminUser.Email = viewModel.AdminUser.Email;
                            adminUser.PhoneNumber = viewModel.AdminUser.PhoneNumber;
                            adminUser.ConfirmPassword = viewModel.AdminUser.ConfirmPassword;
                            var result = _adminUserData.AdminUserSetUpdate(adminUser, "Registration");
                        }
                        else
                        {
                            ViewBag.Error = "Passwords do not match!";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Error = "You already have an account with this email. Please log in or try another email.";
                        return View();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Admin User Registrationn" + ex.Message);
            }
            ViewBag.Success = "Registration completed successfully!";
            return View();

        }

        [HttpGet]
        [UserRoleAuthorize("SuperAdmin")]
        [SessionAuthorize]
        public IActionResult AdminUsers()
        {
            return View();
        }
        [HttpGet]
        [SessionAuthorize]
        public ActionResult GetAdminUsersHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                if (ID != 0)
                {
                    viewModel.AdminUser = _adminUserData.GetAdminUser("", ID);
                    if (viewModel.AdminUser.ID != 0)
                    {
                        return PartialView("_GetAdminUsersPView", viewModel);
                    }
                }
                else
                {
                    return PartialView("_GetAdminUsersPView", viewModel);
                }
            }
            catch (Exception) { }

            return PartialView("_GetAdminUsersPView", viewModel);
        }
        [HttpGet]
        [SessionAuthorize]
        public ActionResult GetAdminUsersDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.AdminUserList = _adminUserData.GetAdminUserList();
            }
            catch (Exception) { }

            return PartialView("_GetAdminUsersDetPView", viewModel);
        }
        [HttpPost]
        [SessionAuthorize]
        public IActionResult AdminUserSetUpdate(AdminViewModel viewModel, IFormFile ImageFile)
        {
            try
            {
                if (viewModel != null)
                {
                    AdminUserMDL adminUser = new AdminUserMDL();
                    if (viewModel.AdminUser.ID == 0)
                    {
                        adminUser = _adminUserData.GetAdminUser(viewModel.AdminUser.Email, 0);
                        if (adminUser.ID <= 0)
                        {
                            if (viewModel.AdminUser.Password == viewModel.AdminUser.ConfirmPassword)
                            {
                                adminUser.Name = viewModel.AdminUser.Name;
                                adminUser.UserName = viewModel.AdminUser.UserName;
                                adminUser.EmpID = viewModel.AdminUser.EmpID;
                                adminUser.Email = viewModel.AdminUser.Email;
                                adminUser.PhoneNumber = viewModel.AdminUser.PhoneNumber;
                                adminUser.ConfirmPassword = viewModel.AdminUser.ConfirmPassword;
                                adminUser.Role = viewModel.AdminUser.Role;
                                if (ImageFile != null && ImageFile.Length > 0)
                                {
                                    adminUser.PhotoUrl = UploadImage(adminUser.Name.ToString(), ImageFile);
                                }
                                adminUser.CanInsert = viewModel.AdminUser.CanInsert;
                                adminUser.CanUpdate = viewModel.AdminUser.CanUpdate;
                                adminUser.CanDelete = viewModel.AdminUser.CanDelete;
                                adminUser.IsActive = viewModel.AdminUser.IsActive;
                                adminUser.InsertId = 0;
                                var result = _adminUserData.AdminUserSetUpdate(adminUser, "Registration");
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
                        if (viewModel.AdminUser.Password == viewModel.AdminUser.ConfirmPassword)
                        {
                            adminUser.ID = viewModel.AdminUser.ID;
                            adminUser.Name = viewModel.AdminUser.Name;
                            adminUser.UserName = viewModel.AdminUser.UserName;
                            adminUser.EmpID = viewModel.AdminUser.EmpID;
                            adminUser.Email = viewModel.AdminUser.Email;
                            adminUser.PhoneNumber = viewModel.AdminUser.PhoneNumber;
                            adminUser.ConfirmPassword = viewModel.AdminUser.ConfirmPassword;
                            adminUser.Role = viewModel.AdminUser.Role;
                            if (ImageFile != null && ImageFile.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(viewModel.AdminUser.PhotoUrl))
                                {
                                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", viewModel.AdminUser.PhotoUrl);
                                    if (System.IO.File.Exists(imagePath))
                                    {
                                        System.IO.File.Delete(imagePath);
                                    }

                                }
                                adminUser.PhotoUrl = UploadImage(adminUser.Name.ToString(), ImageFile);
                            }
                            else
                            {
                                adminUser.PhotoUrl = viewModel.AdminUser.PhotoUrl;
                            }
                            adminUser.CanInsert = viewModel.AdminUser.CanInsert;
                            adminUser.CanUpdate = viewModel.AdminUser.CanUpdate;
                            adminUser.CanDelete = viewModel.AdminUser.CanDelete;
                            adminUser.IsActive = viewModel.AdminUser.IsActive;
                            adminUser.InsertId = 0;
                            var result = _adminUserData.AdminUserSetUpdate(adminUser, "Update");
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
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/img/adminUsers");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            imageName = $"../Admin/img/adminUsers/{fileName}";
            return imageName;
        }
    }
}
