using System;

namespace Moedelo.InfrastructureV2.Domain.Extensions;

internal class ExceptionInfo
{
    public string Message { get; }

    public StackTraceInfo TraceInfo { get; }

    public ExceptionInfo InnerExceptionInfo { get; }

    public ExceptionInfo(Exception ex)
    {
        Message = ex.Message;
        TraceInfo = new StackTraceInfo(ex);
        InnerExceptionInfo = ex.InnerException != null ? new ExceptionInfo(ex.InnerException) : null;
    }
}
