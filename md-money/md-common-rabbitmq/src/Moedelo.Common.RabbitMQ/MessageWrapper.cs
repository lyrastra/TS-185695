using System;

namespace Moedelo.Common.RabbitMQ
{
    internal sealed class MessageWrapper<T>
    {
        public T Data { get; set; }

        public uint RepeatCount { get; set; }

        public TimeSpan? Delay { get; set; }

        public AuditSpanContext AuditSpanContext { get; set; }
    }
}