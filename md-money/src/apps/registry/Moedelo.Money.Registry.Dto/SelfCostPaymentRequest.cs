using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Registry.Dto
{
    public class SelfCostPaymentRequestDto
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
        /// Источник платежей: банк/касса
        /// </summary>
        public OperationSource Source { get; set; }

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