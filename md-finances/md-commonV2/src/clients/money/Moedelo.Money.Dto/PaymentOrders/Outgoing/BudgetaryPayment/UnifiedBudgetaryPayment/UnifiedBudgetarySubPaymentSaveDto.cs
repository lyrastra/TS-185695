using System;
using System.Collections.Generic;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetarySubPaymentSaveDto
    {
        /// <summary>
        /// Базовый идентификатор дочерней операции
        /// </summary>
        public long? DocumentBaseId { get; set; }

        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        public int KbkId { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        public BudgetaryPeriodSaveDto Period { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public TaxPostingsSaveDto TaxPostings { get; set; }

        /// <summary>
        /// Идентификатор торгового объекта
        /// </summary>
        public int? TradingObjectId { get; set; }

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        public long? PatentId { get; set; }

        /// <summary>
        /// Связанные инвойсы на покупку (оплата НДС импортируемых товаров и услуг)
        /// </summary>
        public IReadOnlyCollection<DocumentLinkSaveDto> CurrencyInvoices { get; set; } = Array.Empty<DocumentLinkSaveDto>();
    }
}