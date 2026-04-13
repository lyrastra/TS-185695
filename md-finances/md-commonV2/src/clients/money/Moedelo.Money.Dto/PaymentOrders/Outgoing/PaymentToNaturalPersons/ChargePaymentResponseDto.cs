namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class ChargePaymentResponseDto
    {
        /// <summary>
        /// Начисление в ЗП
        /// </summary>
        public long? ChargeId { get; set; }

        /// <summary>
        /// Связь выплаты с начислением (фиксируется в ЗП)
        /// </summary>
        public int? ChargePaymentId { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
