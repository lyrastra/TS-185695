using System;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.Audit.EmptyMock;

internal sealed class EmptyAuditSpan : IAuditSpan
{
    public static readonly EmptyAuditSpan Instance = new();

    private EmptyAuditSpan() { }

    public IAuditSpanContext Context { get; } = EmptyAuditSpanContext.Instance;

    public AuditSpanType Type { get; } = AuditSpanType.InternalCode;

    public string AppName { get; } = string.Empty;

    public string Name { get; } = string.Empty;

    public bool IsNameNormalized { get; } = true;

    public DateTimeOffset StartDateUtc { get; } = DateTimeOffset.MinValue;

    public DateTimeOffset FinishDateUtc { get; } = DateTimeOffset.MaxValue;

    public bool HasError { get; } = false;

    public IReadOnlyDictionary<string, List<object>> Tags { get; } = new Dictionary<string, List<object>>();

    public void AddTag(string tagName, object tagValue) { }

    public void SetError() { }

    public void SetError(Exception ex) { }

    public void Finish() { }

    public void Finish(DateTimeOffset finishDateUtc) { }

    public void SetNormalizedName(string name) { }
}
