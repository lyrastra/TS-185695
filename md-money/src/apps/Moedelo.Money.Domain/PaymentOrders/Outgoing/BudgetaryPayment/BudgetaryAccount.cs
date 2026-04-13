using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryAccount
    {
        public BudgetaryAccountCodes Code { get; set; }

        public string Name { get; set; }

        public string FullNumber { get; set; }

        public BudgetaryPeriodType DefaultPeriodType { get; set; }
    }
}
