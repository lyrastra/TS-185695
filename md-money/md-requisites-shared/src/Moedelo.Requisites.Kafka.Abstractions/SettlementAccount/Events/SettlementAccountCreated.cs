using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.SettlementAccount.Events
{
    public class SettlementAccountCreated : IEntityEventData
    {
        public int Id { get; set; }
    }
}