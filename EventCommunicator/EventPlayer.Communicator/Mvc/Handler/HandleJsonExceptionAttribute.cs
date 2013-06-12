namespace EventPlayer.Communicator.Mvc.Handler
{
    using System.Web.Mvc;

    public class HandleJsonExceptionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.Exception != null)
            {
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new JsonError
                    {
                        Message = filterContext.Exception.Message,
                        StackTrace = filterContext.Exception.StackTrace
                    }
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}