using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.RentPayment
{
    public class RentPaymentPeriodDto
    {
        /// <summary>
        /// Идентификатор платежного документа
        /// </summary>
        public long PaymentBaseId { get; set; }

        /// <summary>
        /// Дата платежного документа
        /// </summary>
        public DateTime PaymentDate { get; set; }

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
