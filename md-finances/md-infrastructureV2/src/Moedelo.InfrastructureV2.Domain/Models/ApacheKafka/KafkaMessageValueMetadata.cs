using System;

namespace Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

public class KafkaMessageValueMetadata
{
    public Guid MessageId { get; set; }

    public DateTime MessageDate { get; set; }
}