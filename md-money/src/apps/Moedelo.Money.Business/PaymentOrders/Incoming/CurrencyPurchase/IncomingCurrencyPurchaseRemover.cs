using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyPurchase)]
    [InjectAsSingleton(typeof(IIncomingCurrencyPurchaseRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class IncomingCurrencyPurchaseRemover : IIncomingCurrencyPurchaseRemover, IConcretePaymentOrderRemover
    {
        private readonly IncomingCurrencyPurchaseApiClient apiClient;
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IncomingCurrencyPurchaseEventWriter writer;

        public IncomingCurrencyPurchaseRemover(
            IncomingCurrencyPurchaseApiClient apiClient,
            IClosedPeriodValidator closedPeriodValidator,
            IncomingCurrencyPurchaseEventWriter writer)
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