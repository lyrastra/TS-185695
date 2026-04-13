namespace Moedelo.Infrastructure.Kafka.Abstractions.Models
{
    public abstract class KafkaMessageValueBase
    {
        public KafkaMessageValueMetadata? Metadata { get; set; }
    }
}