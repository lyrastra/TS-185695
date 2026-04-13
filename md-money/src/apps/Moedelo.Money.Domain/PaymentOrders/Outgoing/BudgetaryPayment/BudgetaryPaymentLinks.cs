using System.Collections.Generic;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentLinks
    {
        public RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> CurrencyInvoices { get; set; }
    }
}