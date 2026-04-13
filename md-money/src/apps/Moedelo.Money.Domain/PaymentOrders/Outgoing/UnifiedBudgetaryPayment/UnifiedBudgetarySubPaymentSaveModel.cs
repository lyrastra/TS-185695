using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.TaxPostings;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetarySubPaymentSaveModel
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        public int  KbkId { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        public BudgetaryPeriod Period { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public TaxPostingsData TaxPostings { get; set; }

        /// <summary>
        /// Идентификатор торгового объекта
        /// </summary>
        public int? TradingObjectId { get; set; }

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        public long? PatentId { get; set; }

        public IReadOnlyCollection<DocumentLinkSaveRequest> CurrencyInvoices { get; set; }

    }
}
