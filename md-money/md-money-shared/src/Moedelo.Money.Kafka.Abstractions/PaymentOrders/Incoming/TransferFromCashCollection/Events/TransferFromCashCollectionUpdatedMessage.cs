using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection.Events
{
    public class TransferFromCashCollectionUpdatedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public bool ProvideInAccounting { get; set; }

        public OperationState OperationState { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public OutsourceState? OutsourceState { get; set; }
    }
}