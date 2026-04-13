using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToNaturalPersons.Events
{
    public class PaymentToNaturalPersonsUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public decimal Sum => EmployeePayments.SelectMany(x => x.ChargePayments).Sum(y => y.Sum);

        public PaymentToNaturalPersonsType PaymentType { get; set; }

        public IReadOnlyCollection<EmployeePayments> EmployeePayments { get; set; }

        public string Destination { get; set; }

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
