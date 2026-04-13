using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    [OperationType(OperationType.PaymentOrderIncomingIncomeFromCommissionAgent)]
    [InjectAsSingleton(typeof(IIncomeFromCommissionAgentRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class IncomeFromCommissionAgentRemover : IIncomeFromCommissionAgentRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IncomeFromCommissionAgentApiClient apiClient;
        private readonly IncomeFromCommissionAgentEventWriter writer;

        public IncomeFromCommissionAgentRemover(
            IClosedPeriodValidator closedPeriodValidator,
            IncomeFromCommissionAgentApiClient apiClient,
            IncomeFromCommissionAgentEventWriter writer)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var paymentOrder = await apiClient.GetAsync(documentBaseId);
            await closedPeriodValidator.ValidateAsync(paymentOrder.Date);
            await apiClient.DeleteAsync(documentBaseId);
            await writer.WriteDeletedEventAsync(paymentOrder, newDocumentBaseId);
        }
    }
}