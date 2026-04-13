using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Money.Kafka.PaymentOrders.Import.Events
{
    public class ImportFailed : IEntityEventData
    {
        public string SourceFileId { get; set; }
    }
}
