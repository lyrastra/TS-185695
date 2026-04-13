using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.Other.Events
{
    public class OtherOutgoingCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        public ContractorBase Contractor { get; set; }

        public long? ContractBaseId { get; set; }

        public string Destination { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        public IReadOnlyCollection<long> AdvanceStatementBaseIds { get; set; } = Array.Empty<long>();
    }
}
