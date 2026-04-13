using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    public interface IBudgetaryPaymentTaxWidgetApiClient
    {
        Task<List<OrderTaxWidgetResponse>> GetBudgetaryTaxPaymentsAsync(BudgetaryPaymentNdsWidgetRequest request);
    }
}