using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount.Events
{
    public class TransferToSettlementAccountDeleted : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }
    }
}
