using System;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Audit.Writers.Kafka
{
    internal sealed class AuditSpanKafkaModel : KafkaMessageValueBase
    {
        public Guid AsyncTraceId { get; set; }

        public Guid TraceId { get; set; }

        public Guid? ParentId { get; set; }

        public Guid CurrentId { get; set; }

        public short CurrentDepth { get; set; }

        public DateTime StartDateUtc { get; set; }

        public DateTime EndDateUtc { get; set; }

        public bool IsError { get; set; }

        public string Name { get; set; }

        public string App { get; set; }

        public string TagsJson { get; set; }

        public byte Type { get; set; }

        /// <summary>
        /// Имя компьютера
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// длительность в мс
        /// </summary>
        public long Duration { get; set; }
    }
}
