using System;

namespace Moedelo.Money.Domain.SelfCostPayments
{
    public class SelfCostPaymentRequest
    {
        /// <summary>
        /// Количество пропущенных записей сначала
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Количество возвращаемых записей
        /// </summary>
        public int Limit { get; set; } = 100;

        /// <summary>
        /// Фильтр по дате: с указанной даты (включительно) 
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Фильтр по дате: до указанной даты (включительно) 
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}