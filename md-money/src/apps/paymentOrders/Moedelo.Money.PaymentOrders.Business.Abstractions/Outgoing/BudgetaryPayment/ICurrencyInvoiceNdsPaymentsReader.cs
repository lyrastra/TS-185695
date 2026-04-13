using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BudgetaryPayment
{
    public interface ICurrencyInvoiceNdsPaymentsReader
    {
        Task<IReadOnlyList<CurrencyInvoiceNdsPayment>> GetByCriteriaAsync(CurrencyInvoiceNdsPaymentsRequest request);
    }
}