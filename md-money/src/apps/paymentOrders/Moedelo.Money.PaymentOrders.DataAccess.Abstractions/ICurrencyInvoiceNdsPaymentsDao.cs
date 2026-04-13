using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface ICurrencyInvoiceNdsPaymentsDao
    {
        Task<IReadOnlyList<CurrencyInvoiceNdsPayment>> GetByCriteriaAsync(int firmId,
            CurrencyInvoiceNdsPaymentsRequest request);
    }
}