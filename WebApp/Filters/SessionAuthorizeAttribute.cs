using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            if (session == null || string.IsNullOrEmpty(session.GetString("AUserId")))
            {
                var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (isAjax)
                {
                    // For AJAX: return 401 Unauthorized (handled in JS)
                    context.Result = new JsonResult(new { success = false, message = "Session expired" })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
                else
                {
                    // For normal: redirect to login
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "SPanel1325",
                        action = "Login"
                    }));
                }
            }
        }
    }

}
