using System.Web.Mvc;

namespace UsingRepository.Areas.Ar
{
    public class ArAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Ar";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Ar_default",
                "Ar/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}