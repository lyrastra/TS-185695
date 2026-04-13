using System;
using System.Collections.Generic;

namespace Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class CurrencyInvoiceNdsPaymentsRequestDto
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
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Поиск по подстроке в номере
        /// </summary>
        public string QueryByNumber { get; set; }

        /// <summary>
        /// Фильтр по идентификатору КБК
        /// </summary>
        public IReadOnlyCollection<int> KbkIds { get; set; }
    }
}