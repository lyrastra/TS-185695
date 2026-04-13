using System;
using System.Web.Http.ExceptionHandling;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.WebApi.Handlers;
using Moedelo.InfrastructureV2.WebApi.Validation.Exceptions;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Handlers;

[InjectAsSingleton(typeof(WebApiNoValidationUnhandledExceptionLogger))]
public sealed class WebApiNoValidationUnhandledExceptionLogger : BaseMoedeloWebApiExceptionLogger
{
    public WebApiNoValidationUnhandledExceptionLogger(ILogger logger) : base(logger)
    {
    }

    public override void Log(ExceptionLoggerContext context)
    {
        if (context.Exception is ValidationFailureException or AggregateException { InnerException: ValidationFailureException })
        {
            // не логируем валидационные исключения как ошибки
            Logger.Debug(Tag, "При обработке запроса произошла ошибка валидации", extraData: context.Exception);

            return;
        }

        base.Log(context);
    }
}
