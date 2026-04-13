namespace Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

public abstract class MoedeloKafkaMessageValueBase : KafkaMessageValueBase
{
    public string Token { get; set; }

    public AuditSpanContext AuditSpanContext { get; set; }
}