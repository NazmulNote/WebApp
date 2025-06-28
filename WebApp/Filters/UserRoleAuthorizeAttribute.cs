using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UserRoleAuthorizeAttribute : Attribute, IActionFilter
    {
        private readonly string[] _allowedRoles;

        public UserRoleAuthorizeAttribute(params string[] roles)
        {
            _allowedRoles = roles;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var userRole = session.GetString("AUserRole");

            if (string.IsNullOrEmpty(userRole) || !_allowedRoles.Contains(userRole))
            {
                // Unauthorized user, redirect to a default page (like Home/Dashboard)
                context.Result = new RedirectToActionResult("Dashboard", "SPanel1325", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Not used, but required by interface
        }
    }
}
