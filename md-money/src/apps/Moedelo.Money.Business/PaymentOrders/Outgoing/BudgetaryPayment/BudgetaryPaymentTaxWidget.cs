using System.Collections.Generic;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models.BudgetaryPayment;
using BudgetaryPaymentTaxWidgetRequest = Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.BudgetaryPaymentTaxWidgetRequest;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentTaxWidget))]
    internal class BudgetaryPaymentTaxWidget : IBudgetaryPaymentTaxWidget
    {
        private readonly BudgetaryPaymentTaxWidgetApiClient apiClient;

        public BudgetaryPaymentTaxWidget(BudgetaryPaymentTaxWidgetApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public Task<IReadOnlyCollection<OrderTaxWidgetResponse>> GetBudgetaryTaxPaymentsAsync(BudgetaryPaymentTaxWidgetRequest request)
        {
            return apiClient.GetBudgetaryTaxPaymentsAsync(request);
        }
    }
}
