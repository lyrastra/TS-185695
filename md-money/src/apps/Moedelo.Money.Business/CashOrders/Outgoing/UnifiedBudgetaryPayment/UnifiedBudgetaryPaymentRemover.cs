using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [OperationType(OperationType.CashOrderOutgoingUnifiedBudgetaryPayment)]
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentRemover))]
    [InjectAsSingleton(typeof(IConcreteCashOrderRemover))]
    internal sealed class UnifiedBudgetaryPaymentRemover : IUnifiedBudgetaryPaymentRemover, IConcreteCashOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly UnifiedBudgetaryPaymentApiClient apiClient;
        private readonly IUnifiedBudgetaryPaymentEventWriter eventWriter;

        public UnifiedBudgetaryPaymentRemover(
            IClosedPeriodValidator closedPeriodValidator,
            UnifiedBudgetaryPaymentApiClient apiClient,
            IUnifiedBudgetaryPaymentEventWriter eventWriter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.apiClient = apiClient;
            this.eventWriter = eventWriter;
        }

        public async Task DeleteAsync(long documentBaseId)
        {
            var cashOrder = await apiClient.GetAsync(documentBaseId);
            await closedPeriodValidator.ValidateAsync(cashOrder.Date);
            var response = await apiClient.DeleteAsync(documentBaseId);
            await eventWriter.WriteDeletedEventAsync(cashOrder, response.DeletedSubPaymentDocumentIds);
        }
    }
}