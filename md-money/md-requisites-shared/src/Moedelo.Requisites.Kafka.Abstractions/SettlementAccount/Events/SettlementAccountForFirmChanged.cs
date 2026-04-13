using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.SettlementAccount.Events
{
    public class SettlementAccountForFirmChanged: IEntityEventData
    {
        public int FirmId { get; set; }
    }
}
