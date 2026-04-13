using System;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers
{
    public class MoneyTransferOperation
    {
        public int Id { get; set; }
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public MoneyDirection Direction { get; set; }
        public int? SettlementAccountId { get; set; }
        public int? MovementSettlementAccountId { get; set; }
        public int? KontragentId { get; set; }
        public string KontragentName { get; set; }
        public int? KontragentSettlementAccountId { get; set; }
        public int? WorkerId { get; set; }
        public string OperationType { get; set; }
        public decimal Sum { get; set; }
        public string Description { get; set; }

        public MoneyBayType MoneyBayType { get; set; }
        public int? PurseId { get; set; }

        // PayDays
        public int PaymentType { get; set; }
        public string BankSettlementAccount { get; set; }

        // LoansThirdParties
        public string Recepient { get; set; }

        // BudgetaryPayment
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }
        public BudgetaryPaymentSubtype BudgetaryPaymentSubtype { get; set; }
        public BudgetaryPaymentFoundation BudgetaryPaymentFoundation { get; set; }
        public string Kbk { get; set; }
        public string Okato { get; set; }
        public string Oktmo { get; set; }
        public string CodeUin { get; set; }
        public int? EnvdTaxAdministrationId { get; set; }
    }
}
