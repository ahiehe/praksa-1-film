using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace praktika1.Filters
{
    public class NotLogedInRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string role = context.HttpContext.Session.GetString("Role");

            if (!string.IsNullOrEmpty(role))
            {
                context.Result = new RedirectToActionResult("Index", "Film", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
