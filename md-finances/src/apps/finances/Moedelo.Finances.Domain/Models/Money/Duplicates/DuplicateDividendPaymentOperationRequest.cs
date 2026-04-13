using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.Finances.Domain.Models.Money.Duplicates
{
    public class DuplicateDividendPaymentOperationRequest : DuplicateOperationRequest
    {
        public PayDaysPaymentType PayDaysPaymentDividendType => PayDaysPaymentType.Dividend;

        public string DividendPaymentOperationType => "PayDaysOperation";
    }
}