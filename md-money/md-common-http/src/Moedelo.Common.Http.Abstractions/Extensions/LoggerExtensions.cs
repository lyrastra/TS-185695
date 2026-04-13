using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;

namespace Moedelo.Common.Http.Abstractions.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 701, Level = LogLevel.Error,
        Message = "При вызове {httpMethod} {uri} произошла ошибка")]
    internal static partial void LogHttpCallError(this ILogger logger, Exception exception, HttpMethod httpMethod, string uri);
    
    [LoggerMessage(EventId = 702, Level = LogLevel.Trace,
        Message = "Вызов {httpMethod} {uri} из {memberName} из файла {filePath}")]
    internal static partial void LogHttpCall(this ILogger logger, HttpMethod httpMethod, string uri, string memberName, string filePath);
    
    internal static void LogHttpCallError<TRequestBody>(
        this ILogger logger,
        Exception exception,
        HttpMethod httpMethod,
        string uri,
        TRequestBody requestBody)
    {
        object extraData = exception is HttpRequestResponseStatusException { Content: { } } httpRequestResponseStatusException
            ? new { Request = requestBody, Response = httpRequestResponseStatusException.Content }
            : requestBody;

        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", extraData)))
        {
            logger.LogHttpCallError(exception, httpMethod, uri);
        }
    }
}
