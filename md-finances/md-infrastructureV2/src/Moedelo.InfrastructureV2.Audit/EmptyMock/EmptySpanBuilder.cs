using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.Audit.EmptyMock;

internal sealed class EmptySpanBuilder : IAuditSpanBuilder
{
    public static readonly EmptySpanBuilder Instance = new();

    private EmptySpanBuilder()
    {
    }

    public IAuditSpanBuilder AsChildOf(IAuditSpanContext parentSpanContext) => this;

    public IAuditSpanBuilder AsChildOf(IAuditSpan parentSpan) => this;

    public IAuditSpanBuilder IgnoreTraceId() => this;

    public IAuditSpanBuilder WithStartDateUtc(DateTimeOffset startDateUtc) => this;

    public IAuditSpanBuilder WithTag(string tagName, object tagValue) => this;

    public IAuditScope Start() => EmptyAuditScope.Instance;

    public bool IsEnabled => false;
}
