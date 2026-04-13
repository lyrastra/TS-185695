using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.Models;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanReturn.Commands
{
    public class ImportDuplicateLoanReturn : IEntityCommandData
    {
        public long DuplicateId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public decimal? LoanInterestSum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        public long ContractBaseId { get; set; }

        /// <summary>
        /// Признак долгострочного займа или кредита
        /// </summary>
        public bool IsLongTermLoan { get; set; }

        public string SourceFileId { get; set; }

        public int? ImportRuleId { get; set; }
    }
}
