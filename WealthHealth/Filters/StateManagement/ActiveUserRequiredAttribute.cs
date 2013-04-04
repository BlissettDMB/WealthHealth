using System.Collections.Generic;
using System.Web.Mvc;
using WealthHealth.Helpers.Html.FlashMessages;
using WealthHealth.Services.StateManagement;

namespace WealthHealth.Filters.StateManagement
{
    public class ActiveUserRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var activeUserService = new ActiveUserService();
            if (activeUserService.VerifyActiveUserSessionExists()) return;
            filterContext.Controller.TempData["flash"] = 
                new List<FlashMessage> { 
                    new FlashMessage
                        {
                            Action = "Index",
                            Controller = "Home",
                            Area = "",
                            Message = "An active logged in user is required for the requested operation. Please log out and try again.",
                            Status = "error"
                        }
                };
            filterContext.HttpContext.Response.Redirect("/");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}