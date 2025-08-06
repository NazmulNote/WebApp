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
                    viewModel.Message = _messageData.GetMessage("Message",ID);
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
        public IActionResult GetMessageDetPView(DateTime? FromDate, DateTime? ToDate)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.MessageList = _messageData.GetMessageList(FromDate,ToDate,"MessageList");
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
                        message.Email = viewModel.Message.Email;
                        message.Subject = viewModel.Message.Subject;
                        message.Body = viewModel.Message.Body;
                        message.IsActive = viewModel.Message.IsActive;
                        message.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));
                        

                        var result = _messageData.MessageInsertUpdate(message, "Insert");
                        return Json(result.ID);
                    }
                    else
                    {
                        // Update
                        message.ID = viewModel.Message.ID;
                        message.Type = "Message";
                        message.Name = viewModel.Message.Name;
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
                MessageMDL message = _messageData.GetMessage("Message", ID);
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
        #region Subscribe------------------------------------------------------Start
        [HttpGet]
        [UserRoleAuthorize("SuperAdmin", "Admin")]
        public IActionResult SubscribeCreate()
        {
            return View();
        }
        public IActionResult GetSubscribeHdrPView(int ID)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                if (ID != 0)
                {
                    viewModel.Message = _messageData.GetMessage("Subscribe", ID);
                    if (viewModel.Message.ID != 0)
                    {
                        return PartialView("_GetSubscribeHdrPView", viewModel);
                    }
                }
                else
                {
                    viewModel.Message = new MessageMDL();
                    viewModel.Message.IsActive = true;
                    return PartialView("_GetSubscribeHdrPView", viewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("" + ex.Message);
            }
            return PartialView("_GetSubscribeHdrPView", viewModel);
        }
        public IActionResult GetSubscribeDetPView(DateTime? FromDate,DateTime? ToDate)
        {
            AdminViewModel viewModel = new AdminViewModel();
            try
            {
                viewModel.MessageList = _messageData.GetMessageList(FromDate,ToDate,"SubscribeList");
            }
            catch (Exception ex) { }
            return PartialView("_GetSubscribeDetPView", viewModel);
        }
        [HttpPost]
        public IActionResult SubscribeSetUpdate(AdminViewModel viewModel)
        {
            try
            {
                if (viewModel != null && viewModel.Message != null)
                {
                    MessageMDL message = new MessageMDL();

                    if (viewModel.Message.ID == 0)
                    {
                        // Insert
                        message.Type = "Subscribe";
                        message.Email = viewModel.Message.Email;
                        message.IsActive = viewModel.Message.IsActive;
                        message.InsertId = Convert.ToInt32(HttpContext.Session.GetString("AUserId"));                      

                        var result = _messageData.MessageInsertUpdate(message, "Insert");
                        return Json(result.ID);
                    }
                    else
                    {
                        // Update
                        message.ID = viewModel.Message.ID;
                        message.Type = "Subscribe";
                        message.Email = viewModel.Message.Email;
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
        public IActionResult SubscribeDelete(int ID)
        {
            try
            {
                MessageMDL message = _messageData.GetMessage("Subscribe", ID);
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
        #endregion ------------------------------------------------------------End
    }
}
