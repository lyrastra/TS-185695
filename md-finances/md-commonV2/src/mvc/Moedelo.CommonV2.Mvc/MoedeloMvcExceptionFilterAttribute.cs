using System.Net;
using System.Web.Mvc;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.CommonV2.Mvc;

[InjectAsSingleton(typeof(MoedeloMvcExceptionFilterAttribute))]
public class MoedeloMvcExceptionFilterAttribute : ActionFilterAttribute, IExceptionFilter
{
    private const string Tag = nameof(MoedeloMvcExceptionFilterAttribute);
    private readonly ILogger logger;

    public MoedeloMvcExceptionFilterAttribute(ILogger logger)
    {
        this.logger = logger;
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        var exception = filterContext.Exception;

        if (exception == null)
        {
            return;
        }
        
        logger.Error(
            Tag,
            message: "Обработка запроса закончилось с ошибкой",
            exception: exception,
            context: GetAuditContext());
    }

    public void OnException(ExceptionContext filterContext)
    {
        if (filterContext.Exception == null)
        {
            return;
        }

        filterContext.ExceptionHandled = true;
        filterContext.RequestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        filterContext.Result = new EmptyResult();
    }

    private static IAuditContext GetAuditContext()
    {
        var userContext = DependencyResolver.Current.GetService<IUserContext>();
        return userContext?.GetAuditContext();
    }
}
