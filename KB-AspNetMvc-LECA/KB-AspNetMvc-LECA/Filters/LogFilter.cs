using KB_AspNetMvc_LECA.Helpers;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace KB_AspNetMvc_LECA.Filters
{
    /// <summary>
    /// Filter for logging.
    /// </summary>
    public class LogFilter : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// The method that will run after the operations done in the Action are completed.
        /// </summary>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Debug.WriteLine("Operation Complete");
        }
        /// <summary>
        /// The method that will run when the Action is accessed.
        /// </summary>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //I added the URL and request type to the log records.
                LogHelper.AddLog(LogHelper.LogTypes.Information, "HTTP Method: " + filterContext.HttpContext.Request.HttpMethod + " | Raw URL: " + filterContext.HttpContext.Request.RawUrl);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}