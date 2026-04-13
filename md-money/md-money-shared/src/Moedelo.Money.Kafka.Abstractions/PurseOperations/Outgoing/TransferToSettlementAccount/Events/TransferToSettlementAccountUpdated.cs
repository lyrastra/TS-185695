using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount.Events
{
    public class TransferToSettlementAccountUpdated : IEntityEventData
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

        /// <summary>
        /// Тип операции, который был до сохранения (если была смена типа)
        /// </summary>
        public OperationType OldOperationType { get; set; }
    }
}
