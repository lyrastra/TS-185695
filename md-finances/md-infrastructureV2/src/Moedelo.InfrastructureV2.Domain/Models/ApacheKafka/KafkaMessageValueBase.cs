namespace Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

public abstract class KafkaMessageValueBase
{
    public KafkaMessageValueMetadata Metadata { get; set; }
}