using Elmah;
using ExceptionContext = System.Web.Mvc.ExceptionContext;
using IExceptionFilter = System.Web.Mvc.IExceptionFilter;

namespace UsingRepository
{
    public class ElmahExceptionLogger : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
            }
        }
    }

}
