namespace Moedelo.Money.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryKbkGetDto
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
        /// Бух.счет для типа налога/вSзноса
        /// </summary>
        public int AccountCode { get; set; }
    }
}
