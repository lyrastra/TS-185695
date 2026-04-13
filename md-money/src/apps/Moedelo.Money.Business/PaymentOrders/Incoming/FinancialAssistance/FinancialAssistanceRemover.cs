using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.FinancialAssistance
{
    [OperationType(OperationType.PaymentOrderIncomingFinancialAssistance)]
    [InjectAsSingleton(typeof(IFinancialAssistanceRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class FinancialAssistanceRemover : IFinancialAssistanceRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly FinancialAssistanceApiClient apiClient;
        private readonly FinancialAssistanceEventWriter writer;

        public FinancialAssistanceRemover(
            IClosedPeriodValidator closedPeriodValidator,
            FinancialAssistanceApiClient apiClient,
            FinancialAssistanceEventWriter writer)
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