using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Events
{
    public class PaymentToAccountablePersonUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Worker;

        public IReadOnlyCollection<long> AdvanceStatementBaseIds { get; set; } = Array.Empty<long>();

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }

        /// <summary>
        /// Признак: пользователь заполнил НУ вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}