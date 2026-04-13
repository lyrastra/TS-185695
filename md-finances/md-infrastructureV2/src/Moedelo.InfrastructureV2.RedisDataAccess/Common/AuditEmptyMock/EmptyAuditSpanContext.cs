using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common.AuditEmptyMock
{
    internal class EmptyAuditSpanContext : IAuditSpanContext
    {
        private static readonly Lazy<EmptyAuditSpanContext> lazy =
            new Lazy<EmptyAuditSpanContext>(() => new EmptyAuditSpanContext());

        private EmptyAuditSpanContext()
        {
        }

        public static EmptyAuditSpanContext Instance => lazy.Value;

        public Guid AsyncTraceId { get; } = Guid.Empty;

        public Guid TraceId { get; } = Guid.Empty;

        public Guid? ParentId { get; } = Guid.Empty;

        public Guid CurrentId { get; } = Guid.Empty;

        public short CurrentDepth { get; } = 0;
    }
}