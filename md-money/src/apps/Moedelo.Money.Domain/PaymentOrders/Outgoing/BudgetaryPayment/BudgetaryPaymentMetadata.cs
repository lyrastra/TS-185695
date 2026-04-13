using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentMetadata
    {
        public IReadOnlyCollection<BudgetaryAccount> Accounts { get; set; }
        public IReadOnlyCollection<BudgetaryPaymentReason> PaymentReasons { get; set; }
        public IReadOnlyCollection<BudgetaryPaymentReason> PaymentSubReasons { get; set; }
        public IReadOnlyCollection<BudgetaryStatusOfPayer> StatusOfPayers { get; set; }
    }
}
