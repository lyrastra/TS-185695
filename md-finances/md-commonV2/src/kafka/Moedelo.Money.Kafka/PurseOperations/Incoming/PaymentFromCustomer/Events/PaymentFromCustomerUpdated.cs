using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.PurseOperations.Incoming.PaymentFromCustomer.Events
{
    public class PaymentFromCustomerUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int PurseId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        public long? ContractBaseId { get; set; }

        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; } = Array.Empty<DocumentLink>();

        public IReadOnlyCollection<BillLink> BillLinks { get; set; } = Array.Empty<BillLink>();

        /// <summary>
        /// Признак: пользователь заполнил НУ вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }

        /// <summary>
        /// Признак: учитывать в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}
