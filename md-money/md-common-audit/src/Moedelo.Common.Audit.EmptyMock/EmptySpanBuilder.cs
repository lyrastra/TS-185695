using System;
using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.Audit.EmptyMock;

public sealed class EmptySpanBuilder : IAuditSpanBuilder
{
    private static readonly Lazy<EmptySpanBuilder> Lazy =
        new Lazy<EmptySpanBuilder>(() => new EmptySpanBuilder());

    private EmptySpanBuilder()
    {
    }

    public static EmptySpanBuilder Instance => Lazy.Value;

    public IAuditSpanBuilder AsChildOf(IAuditSpanContext parentSpanContext)
    {
        return this;
    }

    public IAuditSpanBuilder AsChildOf(IAuditSpan parentSpan)
    {
        return this;
    }

    public IAuditSpanBuilder IgnoreTraceId()
    {
        return this;
    }

    public IAuditSpanBuilder WithStartDateUtc(DateTime startDateUtc)
    {
        return this;
    }

    public IAuditSpanBuilder WithTag(string tagName, object tagValue)
    {
        return this;
    }

    public IAuditScope Start()
    {
        return EmptyAuditScope.Instance;
    }

    public bool IsEnabled => false;
}
