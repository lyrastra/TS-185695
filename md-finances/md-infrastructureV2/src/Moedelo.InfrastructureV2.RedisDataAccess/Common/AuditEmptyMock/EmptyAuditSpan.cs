using System;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common.AuditEmptyMock
{
    internal class EmptyAuditSpan : IAuditSpan
    {
        private static readonly Lazy<EmptyAuditSpan> lazy = 
            new Lazy<EmptyAuditSpan>(() => new EmptyAuditSpan());
        
        private EmptyAuditSpan()
        {
        }
        
        public static EmptyAuditSpan Instance => lazy.Value;
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
}