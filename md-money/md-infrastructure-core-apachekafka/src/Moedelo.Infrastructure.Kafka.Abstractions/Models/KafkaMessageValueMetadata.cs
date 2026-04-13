using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models
{
    public class KafkaMessageValueMetadata
    {
        public Guid MessageId { get; set; }

        public DateTime MessageDate { get; set; }

        public int? Partition { get; set; }

        public long? Offset { get; set; }
    }
}