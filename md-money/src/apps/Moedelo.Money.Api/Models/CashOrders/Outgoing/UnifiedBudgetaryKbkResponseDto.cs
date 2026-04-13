using Moedelo.Money.Enums.CashOrders;

namespace Moedelo.Money.Api.Models.CashOrders.Outgoing
{
    public class UnifiedBudgetaryKbkResponseDto
    {
        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер КБК
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Бух.счет для типа налога/взноса
        /// </summary>
        public UnifiedBudgetaryAccountCodes AccountCode { get; set; }
    }
}
