using System;

namespace Moedelo.Common.ExecutionContext.Client.Exceptions;

public class RetryCountLimitExceededException : Exception
{
    internal RetryCountLimitExceededException(
        ExecutionContextApiMethod apiMethod,
        string uri,
        int maxAttempts,
        Exception innerException
    ) : base($"Исчерпаны все {maxAttempts} попытки вызвать метод {apiMethod}({uri})", innerException)
    {
    }
}
