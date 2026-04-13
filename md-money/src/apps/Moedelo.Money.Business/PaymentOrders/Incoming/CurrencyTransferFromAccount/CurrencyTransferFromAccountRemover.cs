using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyTransferFromAccount)]
    [InjectAsSingleton(typeof(ICurrencyTransferFromAccountRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class CurrencyTransferFromAccountRemover : ICurrencyTransferFromAccountRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly CurrencyTransferFromAccountApiClient transferFromAccountApiClient;
        private readonly CurrencyTransferFromAccountEventWriter transferFromAccountEventWriter;

        public CurrencyTransferFromAccountRemover(
            IClosedPeriodValidator closedPeriodValidator,
            CurrencyTransferFromAccountApiClient transferFromAccountApiClient,
            CurrencyTransferFromAccountEventWriter transferFromAccountEventWriter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.transferFromAccountApiClient = transferFromAccountApiClient;
            this.transferFromAccountEventWriter = transferFromAccountEventWriter;
        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var paymentOrder = await transferFromAccountApiClient.GetAsync(documentBaseId);
            await closedPeriodValidator.ValidateAsync(paymentOrder.Date);
            await transferFromAccountApiClient.DeleteAsync(documentBaseId);
            await transferFromAccountEventWriter.WriteDeletedEventAsync(paymentOrder, newDocumentBaseId);
        }
    }
}