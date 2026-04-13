using System;

namespace Moedelo.Money.Business.Contracts
{
    internal sealed class RentalPaymentItem
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата платежа     
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal PaymentSum { get; set; }

        /// <summary>
        /// Сумма выкупа
        /// </summary>
        public decimal BuyoutSum { get; set; }

        /// <summary>
        /// Сумма аванса
        /// </summary>
        public decimal AdvanceSum { get; set; }
    }
}
