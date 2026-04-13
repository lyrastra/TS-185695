using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetarySubPayment
    {
        public long DocumentBaseId { get; set; }
        
        public long ParentDocumentId { get; set; }

        /// <summary>
        /// КБК
        /// </summary>
        public SubKbk Kbk { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        public BudgetaryPeriod Period { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> CurrencyInvoices { get; set; }

        public int? TradingObjectId { get; set; }

        public long? PatentId { get; set; }

        public bool TaxPostingsInManualMode { get; set; }

    }
}
