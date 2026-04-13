using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Events
{
    public class TransferToAccountUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public int ToSettlementAccountId { get; set; }

        public long? TransferFromAccountBaseId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }
        public OperationState OperationState { get; set; }
        public OutsourceState? OutsourceState { get; set; }
    }
}