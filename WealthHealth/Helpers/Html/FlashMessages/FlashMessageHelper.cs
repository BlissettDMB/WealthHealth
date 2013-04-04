using System.Collections.Generic;
using System.Web.Mvc;

namespace WealthHealth.Helpers.Html.FlashMessages
{
    public static class FlashMessageHelper
    {
        public static void Flash(this Controller controller, FlashMessage flash)
        {
            if (!string.IsNullOrEmpty(flash.Message) && !string.IsNullOrEmpty(flash.Action))
            {
                if (string.IsNullOrEmpty(flash.Area) && controller.ControllerContext != null)
                {
                    flash.Area = (controller.ControllerContext.RouteData.DataTokens["area"] != null) ?
                        controller.ControllerContext.RouteData.DataTokens["area"].ToString() : "";
                }
                else if (flash.Area == null)
                {
                    flash.Area = "";
                }

                if (string.IsNullOrEmpty(flash.Controller) && controller.ControllerContext != null)
                {
                    flash.Controller = (controller.ControllerContext.RouteData.DataTokens["controller"] != null) ?
                        controller.ControllerContext.RouteData.DataTokens["controller"].ToString() : "";
                }
                else if (flash.Controller == null)
                {
                    flash.Controller = "";
                }

                if (string.IsNullOrEmpty(flash.Status))
                {
                    flash.Status = "info";
                }
            
                controller.TempData["flash"] = new List<FlashMessage> { flash };
            }
        }

        public static void Flash(this Controller controller, List<FlashMessage> flashItems)
        {
            var validFlash = new List<FlashMessage>();
            foreach (var flashItem in flashItems)
            {
                if (string.IsNullOrEmpty(flashItem.Area))
                {
                    flashItem.Area = controller.ControllerContext.RouteData.DataTokens["area"].ToString();
                }
                if (string.IsNullOrEmpty(flashItem.Controller))
                {
                    flashItem.Controller = controller.ControllerContext.RouteData.Values["controller"].ToString();
                }
                if (string.IsNullOrEmpty(flashItem.Status))
                {
                    flashItem.Status = "info";
                }
                if (!string.IsNullOrEmpty(flashItem.Message) && !string.IsNullOrEmpty(flashItem.Action))
                {
                    validFlash.Add(flashItem);
                }
            }

            if (validFlash.Count > 0)
            {
                controller.TempData["flash"] = validFlash;
            }

        }

        public static void FlashMessage(this Controller controller, string message, string status, string actionName, string controllerName, string areaName)
        {
            var flash = new FlashMessage
            {
                Message = message,
                Status = status,
                Action = actionName,
                Controller = controllerName,
                Area = areaName
            };

            Flash(controller, flash);
        }

        public static void FlashInfo(this Controller controller, string message, string actionName, string controllerName, string areaName)
        {
            FlashMessage(controller, message, "info", actionName, controllerName, areaName);
        }

        public static void FlashInfo(this Controller controller, string message, string actionName, string controllerName)
        {
            FlashMessage(controller, message, "info", actionName, controllerName, "");
        }

        public static void FlashInfo(this Controller controller, string message, string actionName)
        {
            FlashMessage(controller, message, "info", actionName, "", "");
        }

        public static void FlashWarning(this Controller controller, string message, string actionName, string controllerName, string areaName)
        {
            FlashMessage(controller, message, "warning", actionName, controllerName, areaName);
        }

        public static void FlashWarning(this Controller controller, string message, string actionName, string controllerName)
        {
            FlashMessage(controller, message, "warning", actionName, controllerName, "");
        }

        public static void FlashWarning(this Controller controller, string message, string actionName)
        {
            FlashMessage(controller, message, "warning", actionName, "", "");
        }

        public static void FlashError(this Controller controller, string message, string actionName, string controllerName, string areaName)
        {
            FlashMessage(controller, message, "error", actionName, controllerName, areaName);
        }

        public static void FlashError(this Controller controller, string message, string actionName, string controllerName)
        {
            FlashMessage(controller, message, "error", actionName, controllerName, "");
        }

        public static void FlashError(this Controller controller, string message, string actionName)
        {
            FlashMessage(controller, message, "error", actionName, "", "");
        }

        public static void FlashSuccess(this Controller controller, string message, string actionName, string controllerName, string areaName)
        {
            FlashMessage(controller, message, "success", actionName, controllerName, areaName);
        }

        public static void FlashSuccess(this Controller controller, string message, string actionName, string controllerName)
        {
            FlashMessage(controller, message, "success", actionName, controllerName, "");
        }

        public static void FlashSuccess(this Controller controller, string message, string actionName)
        {
            FlashMessage(controller, message, "success", actionName, "", "");
        }

        public static MvcHtmlString GenerateFlash(this HtmlHelper helper)
        {
            if (helper.ViewContext.TempData["flash"] != null && helper.ViewContext.TempData["flash"].GetType() == typeof(List<FlashMessage>))
            {
                string requestArea = (helper.ViewContext.RouteData.DataTokens["area"] != null) ?
                    helper.ViewContext.RouteData.DataTokens["area"].ToString() : "";
                string requestController = (helper.ViewContext.RouteData.Values["controller"] != null) ?
                    helper.ViewContext.RouteData.Values["controller"].ToString() : "";
                string requestAction = helper.ViewContext.RouteData.Values["action"].ToString();

                var flashItems = (List<FlashMessage>)helper.ViewContext.TempData["flash"];

                string flashString = "";
                foreach (var flashItem in flashItems)
                {
                    if (flashItem.Area == requestArea && flashItem.Controller == requestController && flashItem.Action == requestAction)
                    {
                        var flashDiv = new TagBuilder("div");
                        flashDiv.Attributes["class"] = "flash flash-" + flashItem.Status;
                        var flashMessage = new TagBuilder("p") {InnerHtml = flashItem.Message};
                        flashDiv.InnerHtml += flashMessage.ToString();
                        flashString += flashDiv.ToString();
                    }
                }
                return new MvcHtmlString(flashString);
            }
            return new MvcHtmlString("");
        }
    }
}