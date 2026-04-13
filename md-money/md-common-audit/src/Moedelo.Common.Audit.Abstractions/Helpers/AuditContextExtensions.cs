#nullable enable
using System;
using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Audit.Abstractions.Helpers;

public static class AuditContextExtensions
{
    public static bool IsNullOrEmpty(this IAuditSpanContext? context)
    {
        return context == null 
               || context.AsyncTraceId == Guid.Empty 
               || context.TraceId == Guid.Empty 
               || context.CurrentId == Guid.Empty;
    }

    public static bool IsEmpty(this IAuditSpanContext? context) => context.IsNullOrEmpty();
}