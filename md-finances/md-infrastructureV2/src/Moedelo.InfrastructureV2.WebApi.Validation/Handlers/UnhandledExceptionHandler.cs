using System;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.WebApi.Validation.Exceptions;
using Moedelo.InfrastructureV2.WebApi.Validation.HttpActionResults;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Handlers;

/// <summary>
/// Отделяет ошибки валидации из прочих произошедших ошибок и формирует соответствующий ответ "Ошибка валидации".
/// Если в вашем приложении нет никакой кастомной обработки ошибок, можете использовать этот хэндлер.
/// Если кастомный хэндлер существует, можете пронаследоваться от этого хэндлера либо скопировать из него
/// нехитрую логику обработки валидационных ошибок к себе.
/// </summary>
[InjectAsSingleton(typeof(UnhandledExceptionHandler))]
public class UnhandledExceptionHandler : ExceptionHandler
{
    public override void Handle(ExceptionHandlerContext context)
    {
        if (context.Exception is ValidationFailureException or AggregateException { InnerException: ValidationFailureException })
        {
            var validationException = context.Exception as ValidationFailureException
                                      ?? context.Exception.InnerException as ValidationFailureException;
                
            context.Result = new ValidationFailedResult(context.Request, validationException);
            return;
        }

        context.Result = new InternalServerErrorResult(context.Request);
    }
}