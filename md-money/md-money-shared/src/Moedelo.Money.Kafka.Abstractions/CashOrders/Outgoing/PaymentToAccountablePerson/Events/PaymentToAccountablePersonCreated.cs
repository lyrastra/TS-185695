using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToAccountablePerson.Events
{
    public class PaymentToAccountablePersonCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public decimal Sum { get; set; }

        public ContractorBase Contractor { get; set; }

        public string Destination { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        public IReadOnlyCollection<long> AdvanceStatementBaseIds { get; set; } = Array.Empty<long>();
    }
}
