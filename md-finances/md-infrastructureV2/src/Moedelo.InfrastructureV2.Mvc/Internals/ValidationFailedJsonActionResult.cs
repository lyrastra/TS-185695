using System.Web.Mvc;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.Mvc.Internals;

internal class ValidationFailedJsonActionResult : ActionResult
{
    private readonly HttpError httpError;

    public ValidationFailedJsonActionResult(HttpError httpError)
    {
        this.httpError = httpError;
    }

    public override void ExecuteResult(ControllerContext context)
    {
        context.HttpContext.Response.StatusCode = 422;
        context.HttpContext.Response.StatusDescription = "Request validation failed";
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.Write(httpError.ToJsonString());
    }
}
