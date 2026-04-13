using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.Money
{
    public class FundsBalanceSumDto
    {
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }
        public double OutgoingSum { get; set; }
    }
}