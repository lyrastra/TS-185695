using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Moedelo.Finances.Public.Infrastructure.Web
{
    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            if (context.Response != null)
            {
                context.Response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true
                };
            }
            base.OnActionExecuted(context);
        }
    }
}