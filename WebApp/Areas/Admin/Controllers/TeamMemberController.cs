using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class TeamMemberController : Controller
    {
        private readonly TeamMemberData _teamMemberData;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamMemberController(IWebHostEnvironment webHostEnvironment)
        {
            _teamMemberData = new TeamMemberData();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult TeamMemberCreate()
        {
            return View();
        }
        public IActionResult GetTeamMemberHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                if (ID != 0)
                {
                    viewModel.TeamMember = _teamMemberData.GetTeamMember(ID);
                    if (viewModel.TeamMember != null && viewModel.TeamMember.ID != 0)
                    {
                        return PartialView("_GetTeamMemberHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.TeamMember = new TeamMemberMDL();
                    return PartialView("_GetTeamMemberHdrPView", viewModel);
                }
            }
            catch (Exception ex){}
            return PartialView("_GetTeamMemberHdrPView", viewModel);
        }
        public IActionResult GetTeamMemberDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.TeamMemberList = _teamMemberData.GetTeamMemberList();
            }
            catch (Exception ex){ }
            return PartialView("_GetTeamMemberDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult TeamMemberSetUpdate(AdminViewModel viewModel, IFormFile ImageFile)
        {
            try
            {
                if (viewModel != null)
                {
                    TeamMemberMDL member = new TeamMemberMDL();

                    if (viewModel.TeamMember?.ID == 0) // নতুন এন্ট্রি
                    {
                        member.Name = viewModel.TeamMember.Name;
                        member.Designation = viewModel.TeamMember.Designation;
                        member.Email = viewModel.TeamMember.Email;
                        member.Phone = viewModel.TeamMember.Phone;
                        member.PresentAddress = viewModel.TeamMember.PresentAddress;
                        member.PermanentAddress = viewModel.TeamMember.PermanentAddress;

                        member.NID = viewModel.TeamMember.NID;
                        member.EmployeeCode = viewModel.TeamMember.EmployeeCode;
                        member.JoinDate = viewModel.TeamMember.JoinDate;
                        member.DateOfBirth = viewModel.TeamMember.DateOfBirth;

                        member.Gender = viewModel.TeamMember.Gender;
                        member.BloodGroup = viewModel.TeamMember.BloodGroup;
                        member.MaritalStatus = viewModel.TeamMember.MaritalStatus;
                        member.Nationality = viewModel.TeamMember.Nationality;

                        member.Department = viewModel.TeamMember.Department;
                        member.TeamCategory = viewModel.TeamMember.TeamCategory;
                        member.Skills = viewModel.TeamMember.Skills;
                        member.ShortBio = viewModel.TeamMember.ShortBio;

                        member.Facebook = viewModel.TeamMember.Facebook;
                        member.LinkedIn = viewModel.TeamMember.LinkedIn;
                        member.Twitter = viewModel.TeamMember.Twitter;

                        member.ViewOrder = viewModel.TeamMember.ViewOrder;
                        member.IsActive = viewModel.TeamMember.IsActive;

                        if (ImageFile != null && ImageFile.Length > 0)
                        {
                            member.PhotoUrl = UploadImage(viewModel.TeamMember.Name.ToString(), ImageFile);
                        }

                        member.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        var result = _teamMemberData.TeamMemberInsertUpdate(member, "Insert");
                        return Json(result.ID);
                    }
                    else // আপডেট
                    {
                        member.ID = viewModel.TeamMember.ID;

                        member.Name = viewModel.TeamMember.Name;
                        member.Designation = viewModel.TeamMember.Designation;
                        member.Email = viewModel.TeamMember.Email;
                        member.Phone = viewModel.TeamMember.Phone;
                        member.PresentAddress = viewModel.TeamMember.PresentAddress;
                        member.PermanentAddress = viewModel.TeamMember.PermanentAddress;

                        member.NID = viewModel.TeamMember.NID;
                        member.EmployeeCode = viewModel.TeamMember.EmployeeCode;
                        member.JoinDate = viewModel.TeamMember.JoinDate;
                        member.DateOfBirth = viewModel.TeamMember.DateOfBirth;

                        member.Gender = viewModel.TeamMember.Gender;
                        member.BloodGroup = viewModel.TeamMember.BloodGroup;
                        member.MaritalStatus = viewModel.TeamMember.MaritalStatus;
                        member.Nationality = viewModel.TeamMember.Nationality;

                        member.Department = viewModel.TeamMember.Department;
                        member.TeamCategory = viewModel.TeamMember.TeamCategory;
                        member.Skills = viewModel.TeamMember.Skills;
                        member.ShortBio = viewModel.TeamMember.ShortBio;

                        member.Facebook = viewModel.TeamMember.Facebook;
                        member.LinkedIn = viewModel.TeamMember.LinkedIn;
                        member.Twitter = viewModel.TeamMember.Twitter;

                        member.ViewOrder = viewModel.TeamMember.ViewOrder;
                        member.IsActive = viewModel.TeamMember.IsActive;

                        if (ImageFile != null && ImageFile.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(viewModel.TeamMember.PhotoUrl))
                            {
                                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", viewModel.TeamMember.PhotoUrl);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }
                            member.PhotoUrl = UploadImage(viewModel.TeamMember.Name.ToString(), ImageFile);
                        }
                        else
                        {
                            member.PhotoUrl = viewModel.TeamMember.PhotoUrl;
                        }

                        member.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        var result = _teamMemberData.TeamMemberInsertUpdate(member, "Update");
                        return Json(result.ID);
                    }
                }
            }
            catch (Exception ex){}
            return Json(0);
        }
        [HttpPost]
        public IActionResult TeamMemberDelete(int ID)
        {
            try
            {
                TeamMemberMDL member = _teamMemberData.GetTeamMember(ID);
                if (member != null && member.ID != 0)
                {
                    bool isDeleted = _teamMemberData.TeamMemberDelete(ID);
                    if (!string.IsNullOrEmpty(member.PhotoUrl))
                    {
                        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", member.PhotoUrl);
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
            catch (Exception ex)
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
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/img/teamMember");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            imageName = $"../Admin/img/teamMember/{fileName}";
            return imageName;
        }
    }
}
