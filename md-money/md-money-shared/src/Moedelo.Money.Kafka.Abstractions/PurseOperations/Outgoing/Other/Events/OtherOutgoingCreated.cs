using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.Other.Events
{
    public class OtherOutgoingCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int PurseId { get; set; }

        public ContractorBase Contractor { get; set; }

        public long? ContractBaseId { get; set; }

        public decimal Sum { get; set; }

        public string Comment { get; set; }

        public long? BillBaseId { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }
    }
}
