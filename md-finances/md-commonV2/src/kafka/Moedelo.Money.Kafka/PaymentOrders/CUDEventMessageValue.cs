using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

namespace Moedelo.Money.Kafka.PaymentOrders
{
    public sealed class CUDEventMessageValue : MoedeloKafkaMessageValueBase
    {
        public CUDEventType EventType { get; set; }

        public string EventDataJson { get; set; }
    }

    public enum CUDEventType
    {
        Created = 1,
        Updated = 2,
        Deleted = 3,
        UpdateAfterAccountingStatementCreated = 4
    }
}
