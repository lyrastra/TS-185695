using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions
{
    /// <summary>
    /// Платежи в бюджет для виджета НДС
    /// </summary>
    public interface IPaymentOrderTaxWidgetReader
    {
        /// <summary>
        /// Платежи в бюджет по запросу
        /// </summary>
        Task<IReadOnlyCollection<OrderTaxWidgetResponse>> GetBudgetaryTaxPaymentsAsync(BudgetaryPaymentTaxWidgetRequest request);
    }
}