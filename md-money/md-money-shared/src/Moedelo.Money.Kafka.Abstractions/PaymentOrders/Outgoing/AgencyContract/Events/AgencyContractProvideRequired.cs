using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract.Events
{
    public class AgencyContractProvideRequired : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        public long ContractBaseId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsPaid { get; set; }
    }
}