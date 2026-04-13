using System;
using System.Collections.Generic;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;

namespace Moedelo.Common.Audit.EmptyMock;

public sealed class EmptyAuditSpan : IAuditSpan
{
    private static readonly Lazy<EmptyAuditSpan> Lazy =
        new Lazy<EmptyAuditSpan>(() => new EmptyAuditSpan());

    private EmptyAuditSpan()
    {
    }

    public static EmptyAuditSpan Instance => Lazy.Value;

    public IAuditSpanContext Context { get; } = EmptyAuditSpanContext.Instance;

    public AuditSpanType Type => AuditSpanType.InternalCode;

    public string Host => null;

    public string AppName => string.Empty;

    public string Name => string.Empty;

    public DateTime StartDateUtc => DateTime.MinValue;

    public DateTime FinishDateUtc => DateTime.MaxValue;

    public bool HasError => false;

    public IReadOnlyDictionary<string, List<object>> Tags { get; } = new Dictionary<string, List<object>>();

    public void AddTag(string tagName, object tagValue)
    {
    }

    public void SetError()
    {
    }

    public void SetError(Exception exception)
    {
    }

    public void Finish()
    {
    }

    public void Finish(DateTime finishDateUtc)
    {
    }

    public void SetName(string value)
    {
    }
}