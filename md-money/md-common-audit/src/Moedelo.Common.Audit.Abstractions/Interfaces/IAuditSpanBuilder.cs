using System;

namespace Moedelo.Common.Audit.Abstractions.Interfaces
{
    public interface IAuditSpanBuilder
    {
        IAuditSpanBuilder AsChildOf(IAuditSpanContext parentSpanContext);

        IAuditSpanBuilder AsChildOf(IAuditSpan parentSpan);

        IAuditSpanBuilder IgnoreTraceId();

        IAuditSpanBuilder WithStartDateUtc(DateTime startDateUtc);

        IAuditSpanBuilder WithTag(string tagName, object tagValue);

        IAuditScope Start();
        
        bool IsEnabled { get; }
    }
}