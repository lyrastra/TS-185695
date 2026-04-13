using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Moedelo.InfrastructureV2.WebApi.Filters;

public class WebApiRejectPostAndPutRequestWithNullParameterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        var httpMethod = actionContext.Request.Method;
        if (httpMethod != HttpMethod.Post && httpMethod != HttpMethod.Put)
        {
            return;
        }

        var nullArg = actionContext.ActionArguments.FirstOrDefault(pair => pair.Value == null);

        if (!IsDefaultStructValue(nullArg))
        {
            if (actionContext.ActionArguments.Count == 1)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    $"Empty request body is not allowed.");
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    $"The argument '{nullArg.Key}' cannot be null");
            }
        }
    }

    private static bool IsDefaultStructValue<T>(T value) where T : struct
    {
        return value.Equals(default(T));
    }
}