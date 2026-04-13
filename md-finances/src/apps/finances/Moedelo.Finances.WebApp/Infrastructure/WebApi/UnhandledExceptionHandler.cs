using System.Net;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.Finances.WebApp.Infrastructure.Exceptions;

namespace Moedelo.Finances.WebApp.Infrastructure.WebApi
{
    [InjectAsSingleton]
    public class UnhandledExceptionHandler : ExceptionHandler, IDI
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is ModelValidationException)
            {
                context.Result = new  StatusCodeResult((HttpStatusCode)422, context.Request);
                return;
            }
            context.Result = new InternalServerErrorResult(context.Request);
        }
    }
}