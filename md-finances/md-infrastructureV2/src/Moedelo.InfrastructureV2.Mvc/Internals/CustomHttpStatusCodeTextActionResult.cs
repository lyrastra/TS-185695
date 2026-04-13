using System.Net;
using System.Web.Mvc;

namespace Moedelo.InfrastructureV2.Mvc.Internals;

internal class CustomHttpStatusCodeTextActionResult : ActionResult
{
    private readonly int statusCode;
    private readonly string statusDescription;
    private readonly string message;

    public string ContentType { get; init; } = "application/text";

    public CustomHttpStatusCodeTextActionResult(HttpStatusCode statusCode,
        string statusDescription,
        string message)
    {
        this.message = message;
        this.statusDescription = statusDescription;
        this.statusCode = (int)statusCode;
    }

    public override void ExecuteResult(ControllerContext context)
    {
        context.HttpContext.Response.StatusCode = statusCode;
        context.HttpContext.Response.StatusDescription = statusDescription;;
        context.HttpContext.Response.ContentType = ContentType;
        context.HttpContext.Response.Write(message);
    }
}
