using System.Web.Mvc;
using UsingRepository.Configuration;

namespace UsingRepository
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LocalizationAttribute());
            filters.Add(new ElmahExceptionLogger());
        }
    }
}
