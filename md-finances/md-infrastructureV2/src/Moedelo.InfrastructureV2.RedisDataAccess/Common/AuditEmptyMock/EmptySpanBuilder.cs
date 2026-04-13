using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common.AuditEmptyMock
{
    internal class EmptySpanBuilder : IAuditSpanBuilder
    {
        private static readonly Lazy<EmptySpanBuilder> lazy =
            new Lazy<EmptySpanBuilder>(() => new EmptySpanBuilder());

        private EmptySpanBuilder()
        {
        }

        public static EmptySpanBuilder Instance => lazy.Value;

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

        public IAuditSpanBuilder WithStartDateUtc(DateTimeOffset startDateUtc)
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
}