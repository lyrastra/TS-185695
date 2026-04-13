using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Base
{
    public abstract class MoedeloKafkaMessageValueBase : KafkaMessageValueBase
    {
        public string Token { get; set; }

        // Id записи в монге. Используется только когда размер сообщения превышает 900000 байт
        public string TransmittedRef { get; set; }

        public AuditSpanContext AuditSpanContext { get; set; }
    }
}