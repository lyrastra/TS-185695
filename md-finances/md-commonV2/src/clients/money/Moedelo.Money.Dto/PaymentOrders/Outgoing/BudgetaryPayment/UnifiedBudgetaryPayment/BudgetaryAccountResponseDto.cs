using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment
{
    public class BudgetaryAccountResponseDto
    {
        public BudgetaryAccountCodes Code { get; set; }

        public string Name { get; set; }

        public string FullNumber { get; set; }

        public BudgetaryPeriodType DefaultPeriodType { get; set; }
    }
}
