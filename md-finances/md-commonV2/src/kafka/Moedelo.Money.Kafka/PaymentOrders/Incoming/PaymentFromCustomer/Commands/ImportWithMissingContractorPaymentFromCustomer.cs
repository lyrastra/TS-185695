using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.Models;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.PaymentFromCustomer.Commands
{
    public class ImportWithMissingContractorPaymentFromCustomer : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public Nds Nds { get; set; }

        /// <summary>
        /// Признак посредничества
        /// </summary>
        public bool IsMediation { get; set; }

        /// <summary>
        /// Вознаграждение посредника
        /// </summary>
        public decimal? MediationCommissionSum { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public string SourceFileId { get; set; }

        public int[] ImportRuleIds { get; set; }
    }
}
