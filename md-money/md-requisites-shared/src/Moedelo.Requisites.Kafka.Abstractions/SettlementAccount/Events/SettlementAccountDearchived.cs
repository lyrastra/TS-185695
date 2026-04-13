using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.SettlementAccount.Events
{
    public class SettlementAccountDearchived : IEntityEventData
    {
        public int Id { get; set; }
    }
}