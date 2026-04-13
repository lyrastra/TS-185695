using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyTransferToAccount)]
    [InjectAsSingleton(typeof(ICurrencyTransferToAccountRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class CurrencyTransferToAccountRemover : ICurrencyTransferToAccountRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly CurrencyTransferToAccountApiClient apiClient;
        private readonly CurrencyTransferToAccountEventWriter eventWriter;

        public CurrencyTransferToAccountRemover(
            IClosedPeriodValidator closedPeriodValidator,
            CurrencyTransferToAccountApiClient apiClient,
            CurrencyTransferToAccountEventWriter eventWriter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.apiClient = apiClient;
            this.eventWriter = eventWriter;
        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var paymentOrder = await apiClient.GetAsync(documentBaseId);
            await closedPeriodValidator.ValidateAsync(paymentOrder.Date);
            await apiClient.DeleteAsync(documentBaseId);
            await eventWriter.WriteDeletedEventAsync(paymentOrder, newDocumentBaseId);
        }
    }
}