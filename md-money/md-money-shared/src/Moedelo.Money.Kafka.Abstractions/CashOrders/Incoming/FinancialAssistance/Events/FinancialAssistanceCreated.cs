using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.FinancialAssistance.Events
{
    public class FinancialAssistanceCreated : IEntityEventData
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
    }
}
