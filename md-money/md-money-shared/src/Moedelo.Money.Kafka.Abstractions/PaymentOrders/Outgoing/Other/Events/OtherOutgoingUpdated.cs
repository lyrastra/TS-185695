using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Events
{
    public class OtherOutgoingUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = new Contractor();

        public long? ContractBaseId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }

        public Nds Nds { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}