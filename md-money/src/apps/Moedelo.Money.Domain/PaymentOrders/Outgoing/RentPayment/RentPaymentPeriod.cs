namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment
{
    public class RentPaymentPeriod
    {
        /// <summary>
        /// Идентификатор платежного документа
        /// </summary>
        public long PaymentOrderBaseId { get; set; }

        /// <summary>
        /// Идентификатор периода оплаты из договора
        /// </summary>
        public int RentalPaymentItemId { get; set; }

        /// <summary>
        /// Сумма оплаты
        /// </summary>
        public decimal PaymentSum { get; set; }
    }
}
