using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.Models;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanIssue.Commands
{
    public class ImportWithMissingContractLoanIssue : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Contractor Contractor { get; set; } = Contractor.Kontragent;

        /// <summary>
        /// Признак долгострочного займа или кредита
        /// </summary>
        public bool IsLongTermLoan { get; set; }

        public string SourceFileId { get; set; }

        public int? ImportRuleId { get; set; }
    }
}
