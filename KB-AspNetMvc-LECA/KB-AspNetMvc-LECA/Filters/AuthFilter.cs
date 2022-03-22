using KB_AspNetMvc_LECA.Helpers;
using KB_AspNetMvc_LECA.Models.CustomModels;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static KB_AspNetMvc_LECA.Helpers.AuthenticationHelper;

namespace KB_AspNetMvc_LECA.Filters
{
    /// <summary>
    /// Filter for authentication.
    /// </summary>
    public class AuthFilter : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// The lowest authorization level of the user who can access the corresponding Controller or Action (where the filter is defined).
        /// </summary>
        private int lowestAccessLevel = 0;
        /// <summary>
        /// Constructor method that determines the lowest access level based on the specified user type.
        /// </summary>
        /// <param name="userType">Type of lowest user that can access.</param>
        public AuthFilter(UserTypes userType)
        {
            try
            {
                lowestAccessLevel = (int)userType;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Authentication process. <br/>
        /// If the user does not have access, I directed the user to the Index page on the Home controller.
        /// </summary>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                //I checked the existence of the cookie containing the authentication information.
                if (HttpContext.Current.Request.Cookies["authcookie"]?.Name != null)
                {
                    string val = HttpContext.Current.Request.Cookies["authcookie"].Value;
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(val);
                    CookieModel model = JsonConvert.DeserializeObject<CookieModel>(ticket.UserData);
                    int userAccessLevel = (int)model.UserType; //The user's access level.
                    if (userAccessLevel >= lowestAccessLevel)
                        LogHelper.AddLog(LogHelper.LogTypes.Success_Audit, "Username: " + model.Username + " | Raw URL: " + filterContext.HttpContext.Request.RawUrl);
                    else
                    {
                        LogHelper.AddLog(LogHelper.LogTypes.Failure_Audit, "Username: " + model.Username + " | Raw URL: " + filterContext.HttpContext.Request.RawUrl);
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
                    }
                }
                else
                {
                    LogHelper.AddLog(LogHelper.LogTypes.Warning, "No authorized user information | Raw URL: " + filterContext.HttpContext.Request.RawUrl);
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}