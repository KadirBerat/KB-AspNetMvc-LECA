using KB_AspNetMvc_LECA.Helpers;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace KB_AspNetMvc_LECA
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Services started when the application starts.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //I have defined global filters.
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        /// <summary>
        /// The method to run when an error occurs in the application.
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                LogHelper.AddLog(exception);
                //I redirected to the Index page in the Home controller.
                Response.Redirect("~/Home/Index");
            }
        }
    }
}
