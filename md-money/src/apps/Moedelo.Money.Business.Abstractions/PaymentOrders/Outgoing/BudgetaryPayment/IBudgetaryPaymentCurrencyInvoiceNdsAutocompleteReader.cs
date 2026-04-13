using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentCurrencyInvoiceNdsAutocompleteReader
    {
        Task<IReadOnlyCollection<CurrencyInvoiceNdsPaymentsAutocompleteResponse>> GetAsync(CurrencyInvoiceNdsPaymentsAutocompleteRequest request);
    }
}