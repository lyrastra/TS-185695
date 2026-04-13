using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencySale)]
    [InjectAsSingleton(typeof(IIncomingCurrencySaleRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class IncomingCurrencySaleRemover : IIncomingCurrencySaleRemover, IConcretePaymentOrderRemover
    {
        private readonly IncomingCurrencySaleApiClient apiClient;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IncomingCurrencySaleEventWriter writer;

        public IncomingCurrencySaleRemover(
            IncomingCurrencySaleApiClient apiClient,
            IClosedPeriodValidator closedPeriodValidator,
            IncomingCurrencySaleEventWriter writer)
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