using System;
using Moedelo.Common.Audit.Abstractions.Interfaces;

namespace Moedelo.Common.RabbitMQ
{
    internal sealed class AuditSpanContext : IAuditSpanContext
    {
        public Guid AsyncTraceId { get; set; }

        public Guid TraceId { get; set; }

        public Guid? ParentId { get; set; }

        public Guid CurrentId { get; set; }

        public short CurrentDepth { get; set; }
    }
}