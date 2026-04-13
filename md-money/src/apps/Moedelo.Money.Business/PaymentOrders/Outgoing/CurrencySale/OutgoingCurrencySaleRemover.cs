using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencySale)]
    [InjectAsSingleton(typeof(IOutgoingCurrencySaleRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class OutgoingCurrencySaleRemover : IOutgoingCurrencySaleRemover, IConcretePaymentOrderRemover
    {
        private readonly OutgoingCurrencySaleApiClient apiClient;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly OutgoingCurrencySaleEventWriter writer;

        public OutgoingCurrencySaleRemover(
            OutgoingCurrencySaleApiClient apiClient,
            IClosedPeriodValidator closedPeriodValidator,
            OutgoingCurrencySaleEventWriter writer)
        {
            this.apiClient = apiClient;
            this.closedPeriodValidator = closedPeriodValidator;
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