using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Moedelo.CommonV2.Xss.WebApi;

/// <summary>
/// Фильтр, отклоняющий запросы, в которых обнаружена XSS-атака 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RejectOnXssDetectedAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        try
        {
            foreach (var argument in actionContext.ActionArguments)
            {
                XssValidator.Validate(argument.Value);
            }
        }
        catch (XssValidationException e)
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new
                {
                    Message =
                        $"Potentially dangerous content was detected by paths: {string.Join(", ", e.Suspicious.Path)}"
                });
        }
    }
}
