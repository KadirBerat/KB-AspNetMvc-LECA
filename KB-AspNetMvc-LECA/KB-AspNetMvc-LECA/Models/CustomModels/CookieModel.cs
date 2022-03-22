using static KB_AspNetMvc_LECA.Helpers.AuthenticationHelper;

namespace KB_AspNetMvc_LECA.Models.CustomModels
{
    /// <summary>
    /// The class that defines the data held in the cookie.
    /// </summary>
    public class CookieModel
    {
        /// <summary>
        /// Username data
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The role (authority) the user has.
        /// </summary>
        public UserTypes UserType { get; set; }
        /// <summary>
        /// Current user's ID
        /// </summary>
        public int ID { get; set; }
    }
}