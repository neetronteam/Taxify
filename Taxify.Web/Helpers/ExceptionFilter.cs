using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Taxify.Web.Helpers;

public class ExceptionFilterAttribute : ActionFilterAttribute
{
public override void OnActionExecuting(ActionExecutingContext filterContext)
{
    try
    {
        base.OnActionExecuting(filterContext);
    }
    catch (Exception ex)
    {
        // Handle the exception here
        filterContext.Result = new ViewResult { ViewName = "Error" };
        filterContext.HttpContext.Response.StatusCode = 500;
    }
}
}