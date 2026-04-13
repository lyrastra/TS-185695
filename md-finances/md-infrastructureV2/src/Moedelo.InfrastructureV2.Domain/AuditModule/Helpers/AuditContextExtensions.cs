using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;

public static class AuditContextExtensions
{
    public static bool IsEmpty(this IAuditSpanContext context)
    {
        return context == null 
               || context.AsyncTraceId == Guid.Empty 
               || context.TraceId == Guid.Empty 
               || context.CurrentId == Guid.Empty;
    }
}