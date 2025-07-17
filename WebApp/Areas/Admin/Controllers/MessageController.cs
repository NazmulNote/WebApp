using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.Filters;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class MessageController : Controller
    {
        private readonly MessageData _messageData;
        public MessageController()
        {
            _messageData = new MessageData(); 
        }
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult MessageCreate()
        {
            return View();
        }
        public IActionResult GetMessageHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                if (ID != 0)
                {
                    viewModel.Message = _messageData.GetMessage(ID);
                    if (viewModel.Message.ID != 0)
                    {
                        return PartialView("_GetMessageHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.Message = new MessageMDL();
                    viewModel.Message.IsActive = true;
                    return PartialView("_GetMessageHdrPView", viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
            return PartialView("_GetMessageHdrPView", viewModel);
        }
        public IActionResult GetMessageDetPView()
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.MessageList = _messageData.GetMessageList();
            }
            catch (Exception ex) { }
            return PartialView("_GetMessageDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult MessageSetUpdate(AdminViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.Message != null)
                {
                    MessageMDL message = new MessageMDL();

                    if (viewModel.Message.ID == 0)
                    {
                        // Insert
                        message.Type = "Message";
                        message.Name = viewModel.Message.Name;
                        message.Type = viewModel.Message.Type;
                        message.Email = viewModel.Message.Email;
                        message.Subject = viewModel.Message.Subject;
                        message.Body = viewModel.Message.Body;
                        message.IsActive = viewModel.Message.IsActive;
                        message.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        message.InsertedByIP = HttpContext.Connection.RemoteIpAddress?.ToString();

                        var result = _messageData.MessageInsertUpdate(message, "Insert");
                        return Json(result.ID);
                    }
                    else
                    {
                        // Update
                        message.ID = viewModel.Message.ID;
                        message.Name = viewModel.Message.Name;
                        message.Type = viewModel.Message.Type;
                        message.Email = viewModel.Message.Email;
                        message.Subject = viewModel.Message.Subject;
                        message.Body = viewModel.Message.Body;
                        message.IsActive = viewModel.Message.IsActive;
                        message.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        message.UpdatedAt = DateTime.Now;

                        var result = _messageData.MessageInsertUpdate(message, "Update");
                        return Json(result.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

            return Json(0);
        }
        public IActionResult MessageDelete(int ID)
        {
            try
            {
                MessageMDL message = _messageData.GetMessage(ID);
                if (message.ID != 0)
                {
                    bool isDeleted = _messageData.MessageDelete(ID);
                    return Json(isDeleted ? 1 : 0);
                }
                else
                {
                    return Json(0); // Not found
                }
            }
            catch (Exception ex)
            {
                return Json(0); // Error occurred
            }
        }

    }
}
