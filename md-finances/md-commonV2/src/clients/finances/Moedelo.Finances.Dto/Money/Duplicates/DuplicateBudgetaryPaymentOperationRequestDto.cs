using System;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class DuplicateBudgetaryPaymentOperationRequestDto
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string PaymentOrderNumber { get; set; }
        public int? SettlementAccountId { get; set; }
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }
        public BudgetaryPaymentSubtype? BudgetaryPaymentSubType { get; set; }
    }
}