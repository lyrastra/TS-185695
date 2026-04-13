using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;
using BudgetaryPaymentTaxWidgetRequest = Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.BudgetaryPaymentTaxWidgetRequest;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment
{
    /// <summary>
    /// Платежи в бюджет для виджета НДС
    /// </summary>
    public interface IBudgetaryPaymentTaxWidget
    {
        /// <summary>
        /// Бюджетные Налоговые Платежи
        /// </summary>
        Task<IReadOnlyCollection<OrderTaxWidgetResponse>> GetBudgetaryTaxPaymentsAsync(BudgetaryPaymentTaxWidgetRequest request);
    }}
