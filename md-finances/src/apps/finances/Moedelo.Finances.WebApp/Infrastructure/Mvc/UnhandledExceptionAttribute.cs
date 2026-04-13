using System.Net;
using System.Web.Mvc;

namespace Moedelo.Finances.WebApp.Infrastructure.Mvc
{
    public class UnhandledExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}