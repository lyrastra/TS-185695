
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class SubKbk
    {
        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер КБК (104)
        /// </summary>
        public string Number { get; set; }

        public BudgetaryAccountCodes? AccountCode { get; set; }

        public KbkPaymentType? KbkPaymentType { get; set; }
    }
}
