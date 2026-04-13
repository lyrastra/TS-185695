using System;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.Domain.Extensions;

public static class ExceptionExtensions
{
    public static string GetExceptionInfo(this Exception ex)
    {
        try
        {
            var info = new ExceptionInfo(ex);
            return info.ToJsonString();
        }
        catch
        {
            return string.Empty;
        }
    }
        
    public static string GetStackTraceInfo(this Exception ex)
    {
        try
        {
            var info = new ExceptionStackTraceInfo(ex);
            return info.ToJsonString();
        }
        catch
        {
            return string.Empty;
        }
    }
        
    private class ExceptionStackTraceInfo
    {
        public StackTraceInfo TraceInfo { get; }

        public ExceptionStackTraceInfo InnerExceptionTraceInfo { get; }

        public ExceptionStackTraceInfo(Exception ex)
        {
            TraceInfo = new StackTraceInfo(ex);
            InnerExceptionTraceInfo = ex.InnerException?.StackTrace != null ? new ExceptionStackTraceInfo(ex.InnerException) : null;
        }
    }
}