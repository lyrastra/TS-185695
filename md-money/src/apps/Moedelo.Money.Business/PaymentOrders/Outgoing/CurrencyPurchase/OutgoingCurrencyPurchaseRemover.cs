using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyPurchase)]
    [InjectAsSingleton(typeof(IOutgoingCurrencyPurchaseRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class OutgoingCurrencyPurchaseRemover : IOutgoingCurrencyPurchaseRemover, IConcretePaymentOrderRemover
    {
        private readonly OutgoingCurrencyPurchaseApiClient apiClient;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly OutgoingCurrencyPurchaseEventWriter writer;

        public OutgoingCurrencyPurchaseRemover(
            OutgoingCurrencyPurchaseApiClient apiClient,
            IClosedPeriodValidator closedPeriodValidator,
            OutgoingCurrencyPurchaseEventWriter writer)
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