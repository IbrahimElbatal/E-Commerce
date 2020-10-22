using System.Globalization;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace UsingRepository.Configuration
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var url = filterContext.HttpContext.Request.Url.AbsoluteUri;
            if (url.ToLower().Contains("ar"))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-eg");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-eg");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            }
        }
    }
}