using System;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.AuditMvc.Internals;

internal readonly struct MvcAuditTrailScope
{
    private MvcAuditTrailScope(IAuditScope scope, MvcAuditTrailScopeType type)
    {
        Scope = scope;
        Type = type;
    }

    public IAuditScope Scope { get; }
    public MvcAuditTrailScopeType Type { get; }

    internal static MvcAuditTrailScope ActionExecuting(IAuditScope scope) => new(scope, MvcAuditTrailScopeType.ActionExecuting);
    internal static MvcAuditTrailScope ResultExecuting(IAuditScope scope) => new(scope, MvcAuditTrailScopeType.ResultExecuting);
    internal static readonly MvcAuditTrailScope Null = new (EmptyAuditScope.Instance, MvcAuditTrailScopeType.Null);

    private class EmptyAuditScope : IAuditScope
    {
        private EmptyAuditScope()
        {
        }

        public static readonly EmptyAuditScope Instance = new ();

        public IAuditSpan Span => EmptyAuditSpan.Instance;

        public bool IsEnabled => false;

        public void Dispose()
        {
        }
    }
    
    private class EmptyAuditSpan : IAuditSpan
    {
        private EmptyAuditSpan()
        {
        }
        
        public static readonly EmptyAuditSpan Instance = new ();
        public IAuditSpanContext Context { get; } = EmptyAuditSpanContext.Instance;

        public AuditSpanType Type { get; } = AuditSpanType.InternalCode;

        public string AppName { get; } = string.Empty;

        public string Name { get; } = string.Empty;

        public bool IsNameNormalized { get; } = true;

        public DateTimeOffset StartDateUtc { get; } = DateTimeOffset.MinValue;

        public DateTimeOffset FinishDateUtc { get; } = DateTimeOffset.MaxValue;

        public bool HasError { get; } = false;

        public IReadOnlyDictionary<string, List<object>> Tags { get; } = new Dictionary<string, List<object>>();
        
        public void AddTag(string tagName, object tagValue)
        {
        }

        public void SetError()
        {
        }

        public void SetError(Exception ex)
        {
        }

        public void Finish()
        {
        }

        public void Finish(DateTimeOffset finishDateUtc)
        {
        }

        public void SetNormalizedName(string name)
        {
        }
    }
    
    private class EmptyAuditSpanContext : IAuditSpanContext
    {
        private EmptyAuditSpanContext()
        {
        }

        public static readonly EmptyAuditSpanContext Instance = new();

        public Guid AsyncTraceId { get; } = Guid.Empty;

        public Guid TraceId { get; } = Guid.Empty;

        public Guid? ParentId { get; } = Guid.Empty;

        public Guid CurrentId { get; } = Guid.Empty;

        public short CurrentDepth { get; } = 0;
    }
}
