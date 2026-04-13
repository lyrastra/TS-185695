using System;
using Moedelo.Common.ExecutionContext.Abstractions.Enums;

namespace Moedelo.Common.ExecutionContext.Abstractions.Exceptions;

public class ExecutionContextInitializationFailedException : Exception
{
    public ExecutionContextInitializationError ErrorCode { get; }
        
    public ExecutionContextInitializationFailedException(ExecutionContextInitializationError errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public ExecutionContextInitializationFailedException(
        ExecutionContextInitializationError errorCode,
        string message,
        Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}