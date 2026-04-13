using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromCash.Events
{
    public class TransferFromCashUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public long FromCashId { get; set; }

        public decimal Sum { get; set; }

        public ContractorBase Contractor { get; set; }

        public string Destination { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Тип кассового ордера, который был до сохранения (если была смена типа)
        /// </summary>
        public OperationType OldOperationType { get; set; }
    }
}
