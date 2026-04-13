using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.Finances.Domain.Models.Money.Duplicates
{
    public class DuplicateBudgetaryPaymentOperationRequest : DuplicateOperationRequest
    {
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }
        public BudgetaryPaymentSubtype? BudgetaryPaymentSubType { get; set; }

        public string BudgetaryPaymentOperationType => "BudgetaryPaymentOperation";
    }
}