using System;

namespace Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

public interface IAuditSpanBuilder
{
    IAuditSpanBuilder AsChildOf(IAuditSpanContext parentSpanContext);

    IAuditSpanBuilder AsChildOf(IAuditSpan parentSpan);

    IAuditSpanBuilder IgnoreTraceId();

    IAuditSpanBuilder WithStartDateUtc(DateTimeOffset startDateUtc);

    IAuditSpanBuilder WithTag(string tagName, object tagValue);

    IAuditScope Start();

    bool IsEnabled { get; }
}