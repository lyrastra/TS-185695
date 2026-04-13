namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkSaveDto
    {
        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Номер КБК (104)
        /// </summary>
        public string Number { get; set; }
    }
}
