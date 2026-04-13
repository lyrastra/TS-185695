using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other.Events
{
    public class OtherIncomingCreatedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = new Contractor();

        public long? ContractBaseId { get; set; }

        public IReadOnlyCollection<BillLink> BillLinks { get; set; } = Array.Empty<BillLink>();

        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        /// <summary>
        /// Выбранная СНО в операции
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public OperationState OperationState { get; set; }

        /// <summary>
        /// Признак: целевое поступление
        /// </summary>
        public bool IsTargetIncome { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        /// <summary>
        /// Идентификаторы применённых правил импорта
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        /// <summary>
        /// Идентификатор лога импорта
        /// </summary>
        public int? ImportLogId { get; set; }
    }
}
