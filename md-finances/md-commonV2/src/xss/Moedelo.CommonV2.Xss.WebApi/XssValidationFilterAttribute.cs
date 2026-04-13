using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Moedelo.CommonV2.Xss.WebApi.Extensions;

namespace Moedelo.CommonV2.Xss.WebApi;

/// <summary>
/// Фильтр, отклоняющий запросы, в которых обнаружена XSS-атака (с возможностью настройки режима реакции).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class XssValidationFilterAttribute : ActionFilterAttribute
{
    public XssValidationMode Mode { get; set; } = XssValidationMode.ThrowXssException;

    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        try
        {
            foreach (var argument in actionContext.ActionArguments)
            {
                XssValidator.Validate(argument.Value);
            }
        }
        catch (XssValidationException e) when (Mode == XssValidationMode.RejectSuspiciousRequests)
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
