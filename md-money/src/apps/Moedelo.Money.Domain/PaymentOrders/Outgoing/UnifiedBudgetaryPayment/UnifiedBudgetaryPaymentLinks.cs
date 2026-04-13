using System.Collections.Generic;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    public class UnifiedBudgetaryPaymentLinks
    {
        public RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> CurrencyInvoices { get; set; }
    }
}