
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryKbkResponseDto
    {
        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер КБК (104)
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Бух.счет
        /// </summary>
        public BudgetaryAccountCodes AccountCode { get; set; }
    }
}
