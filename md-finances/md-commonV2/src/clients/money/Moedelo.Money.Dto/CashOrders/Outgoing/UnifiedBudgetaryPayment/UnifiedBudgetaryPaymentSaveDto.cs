using System;
using System.Collections.Generic;

namespace Moedelo.Money.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    /// <summary>
    /// Модель для сохранения операции "Единый налоговый платеж (ЕНП)"
    /// </summary>
    public class UnifiedBudgetaryPaymentSaveDto
    {
        /// <summary>
        /// Базовый идентификатор документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Идентификатор кассы
        /// </summary>
        public long CashId { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Назначение
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Дочерние платежи
        /// </summary>
        public IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveDto> SubPayments { get; set; }
    }
}