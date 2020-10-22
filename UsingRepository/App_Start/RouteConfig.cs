using System.Web.Mvc;
using System.Web.Routing;

namespace UsingRepository
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Products", action = "HomePage", id = UrlParameter.Optional }
            );


            #region another route
            //            routes.MapRoute(
            //                    name: "DeleteRoute",
            //                    url: "{controller}/{action}/{concurrencyError}",
            //                    defaults: new { /*controller = "Products", action = "HomePage",*/ /*concurrencyError = false*/ }
            //                ); 
            #endregion

        }
    }
}
