using KB_AspNetMvc_LECA.Models.CustomModels;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Security;

namespace KB_AspNetMvc_LECA.Helpers
{
    /// <summary>
    /// Authentication processes.
    /// </summary>
    public class AuthenticationHelper
    {
        /// <summary>
        /// Default user types.
        /// </summary>
        public enum UserTypes
        {
            User = 0,
            Admin = 1,
            MasterAdmin = 2,
            Tester = 3,
            Developer = 4
        }
        /// <summary>
        /// Method that returns the current user type.
        /// </summary>
        /// <param name="type">The user type for the current user.</param>
        /// <returns>Returns the user type in String.</returns>
        private static string GetUserType(UserTypes type)
        {
            try
            {
                string userType = String.Empty;
                switch (type)
                {
                    case UserTypes.User:
                        userType = "currentuser";
                        break;
                    case UserTypes.Admin:
                        userType = "admin";
                        break;
                    case UserTypes.MasterAdmin:
                        userType = "madmin";
                        break;
                    case UserTypes.Tester:
                        userType = "teste";
                        break;
                    case UserTypes.Developer:
                        userType = "developer";
                        break;
                    default:
                        userType = "currentuser";
                        break;
                }
                return userType;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "#!";
            }
        }
        /// <summary>
        /// The process of adding an authentication cookie.
        /// </summary>
        /// <param name="model">The model containing the user information to be added to the cookie.</param>
        public static void AddAuthCookie(CookieModel model)
        {
            try
            {
                string cookieName = GetUserType(model.UserType);
                if (cookieName != "#!")
                {
                    int typeInt = (int)model.UserType;
                    string mdl = JsonConvert.SerializeObject(model);
                    DateTime expire = DateTime.Now.AddDays(1);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(typeInt, cookieName, DateTime.Now, expire, false, mdl);
                    string hashTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie("authcookie", hashTicket);
                    cookie.Expires = expire;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Response.Flush();
                    LogHelper.AddLog(LogHelper.LogTypes.Information, "'authcookie' Created");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// The process of deleting an authentication cookie.
        /// </summary>
        public static void DeleteAuthCookie()
        {
            try
            {
                if (HttpContext.Current.Request.Cookies["authcookie"] != null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["authcookie"];
                    DateTime expire = DateTime.Now.AddDays(-1);
                    cookie.Expires = expire;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Response.Flush();
                    LogHelper.AddLog(LogHelper.LogTypes.Information, "'authcookie' Deleted");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}