using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.Models;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.RefundToCustomer.Commands
{
    public class ImportDuplicateRefundToCustomer : IEntityCommandData
    {
        public long DuplicateId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        public long ContractBaseId { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        /// <summary>
        /// Учитывать в СНО
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public string SourceFileId { get; set; }

        public int? ImportRuleId { get; set; }
    }
}
