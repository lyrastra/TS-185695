using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Money.Kafka.PurseOperations.Incoming.PaymentFromCustomer.Events
{
    public class PaymentFromCustomerDeleted : IEntityEventData
    {
        public long DocumentBaseId { get; set; }
    }
}
