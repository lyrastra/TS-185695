using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Extensions;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.InfrastructureV2.WebApi.Handlers
{
    [Inject(InjectionType.Singleton)]
    public class UnhandledExceptionHandler : ExceptionHandler, IDI
    {
        public override void Handle(ExceptionHandlerContext context)
        {
#if DEBUG
            context.Result = new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(
                    context.Exception.GetExceptionInfo(),
                    Encoding.UTF8,
                    "application/json")
            });
#else
            context.Result = new InternalServerErrorResult(context.Request);
#endif
        }
    }
}
