using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount.Events
{
    public class TransferToSettlementAccountCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int PurseId { get; set; }

        public decimal Sum { get; set; }

        public int? SettlementAccountId { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }
    }
}
