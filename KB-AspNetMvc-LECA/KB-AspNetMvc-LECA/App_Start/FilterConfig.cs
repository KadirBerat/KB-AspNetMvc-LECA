using KB_AspNetMvc_LECA.Filters;
using System.Web.Mvc;

namespace KB_AspNetMvc_LECA
{
    /// <summary>
    /// FilterConfig class.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Adds a new filter to the global filters collection.
        /// </summary>
        /// <param name="filters">Global filter collection.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //I added the LogFilter class to the GlobalFilter collection.
            filters.Add(new LogFilter());
        }
    }
}