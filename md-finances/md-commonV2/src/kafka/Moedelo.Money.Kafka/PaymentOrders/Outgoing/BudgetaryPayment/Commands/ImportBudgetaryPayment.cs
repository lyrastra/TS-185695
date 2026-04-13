using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.Common.Enums.Enums.KbkNumbers;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.BudgetaryPayment.Models;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.BudgetaryPayment.Commands
{
    public class ImportBudgetaryPayment : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public Common.Enums.Enums.Accounting.BudgetaryPaymentType PaymentType { get; set; }

        public SyntheticAccountCode AccountCode { get; set; }

        public int? KbkId { get; set; }

        public string KbkNumber { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }

        public BudgetaryPeriod Period { get; set; }

        public BudgetaryPayerStatus PayerStatus { get; set; }

        public BudgetaryPaymentBase PaymentBase { get; set; }

        public string DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public string Uin { get; set; }

        public BudgetaryRecipient Recipient { get; set; }

        public int? TradingObjectId { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public string SourceFileId { get; set; }

        public int? ImportRuleId { get; set; }
    }
}
