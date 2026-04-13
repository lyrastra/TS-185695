using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Events
{
    public class PaymentToNaturalPersonsCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum => EmployeePayments.SelectMany(x => x.ChargePayments).Sum(y => y.Sum);

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public PaymentToNaturalPersonsType PaymentType { get; set; }

        public IReadOnlyCollection<EmployeePayments> EmployeePayments { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        /// <summary>
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        public bool IsSaveNumeration { get; set; }

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
