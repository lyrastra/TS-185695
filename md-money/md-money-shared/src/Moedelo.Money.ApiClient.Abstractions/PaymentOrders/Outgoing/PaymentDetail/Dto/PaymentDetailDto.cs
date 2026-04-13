namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto
{
    /// <summary>
    /// Детали получателя платежа при списании
    /// </summary>
    public class PaymentDetailDto
    {
        /// <summary>
        /// Идентификаторы сотрудников
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// Номер счета получателя
        /// </summary>
        public string PayeeSettlementAccount { get; set; }

        /// <summary>
        /// Идентификатор счета получателя
        /// </summary>
        public int? PayeeSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Номер платежа
        /// </summary>
        public string Number { get; set; }
    }
}