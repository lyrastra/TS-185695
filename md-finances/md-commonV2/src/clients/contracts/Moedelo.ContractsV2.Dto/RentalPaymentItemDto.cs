using System;

namespace Moedelo.ContractsV2.Dto
{
    public class RentalPaymentItemDto
    {
        public long ContractBaseId { get; set; }

        public long Id { get; set; }

        /// <summary>
        /// Номер платежа
        /// </summary>
        public byte Number { get; set; }

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
