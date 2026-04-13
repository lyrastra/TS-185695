using System;

namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostPayments.AdvanceStatements.Models
{
    /// <summary>
    /// Платеж (АО) для расчетов себестоимости
    /// </summary>
    public class AdvanceStatemenSelfCostPaymentDto
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер платежа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }
    }
}