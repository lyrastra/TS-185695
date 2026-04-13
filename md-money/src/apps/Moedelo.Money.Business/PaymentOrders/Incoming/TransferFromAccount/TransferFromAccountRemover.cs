using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount
{
    [OperationType(OperationType.PaymentOrderIncomingTransferFromAccount)]
    [InjectAsSingleton(typeof(ITransferFromAccountRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class TransferFromAccountRemover : ITransferFromAccountRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly TransferFromAccountApiClient transferFromAccountApiClient;
        private readonly TransferToAccountApiClient transferToAccountApiClient;
        private readonly TransferFromAccountEventWriter transferFromAccountEventWriter;
        private readonly TransferToAccountEventWriter transferToAccountEventWriter;
        private readonly TransferFromAccountLinksGetter linksGetter;

        public TransferFromAccountRemover(
            IClosedPeriodValidator closedPeriodValidator,
            TransferFromAccountApiClient transferFromAccountApiClient,
            TransferToAccountApiClient transferToAccountApiClient,
            TransferFromAccountEventWriter transferFromAccountEventWriter,
            TransferToAccountEventWriter transferToAccountEventWriter,
            TransferFromAccountLinksGetter linksGetter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.transferFromAccountApiClient = transferFromAccountApiClient;
            this.transferToAccountApiClient = transferToAccountApiClient;
            this.transferFromAccountEventWriter = transferFromAccountEventWriter;
            this.transferToAccountEventWriter = transferToAccountEventWriter;
            this.linksGetter = linksGetter;
        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var transferToAccountLink = await GetLinkedPaymentAsync(documentBaseId);
            if (transferToAccountLink != null)
            {
                await closedPeriodValidator.ValidateAsync(transferToAccountLink.Date);
            }

            var paymentOrder = await transferFromAccountApiClient.GetAsync(documentBaseId);
            await closedPeriodValidator.ValidateAsync(paymentOrder.Date);

            await DeleteLinkedPaymentAsync(transferToAccountLink?.DocumentBaseId);
            await DeletePaymentAsync(paymentOrder, newDocumentBaseId);
        }

        private async Task<PaymentOrderLink> GetLinkedPaymentAsync(long documentBaseId)
        {
            var links = await linksGetter.GetAsync(documentBaseId);
            return links.TransferToAccount.GetOrThrow();
        }

        private async Task DeletePaymentAsync(TransferFromAccountResponse response, long? newDocumentBaseId)
        {
            await transferFromAccountApiClient.DeleteAsync(response.DocumentBaseId);
            await transferFromAccountEventWriter.WriteDeletedEventAsync(response, newDocumentBaseId);
        }

        private async Task DeleteLinkedPaymentAsync(long? linkedPaymentBaseId)
        {
            if (linkedPaymentBaseId.HasValue)
            {
                var paymentOrder = await transferToAccountApiClient.GetAsync(linkedPaymentBaseId.Value);
                await transferToAccountApiClient.DeleteAsync(linkedPaymentBaseId.Value);
                await transferToAccountEventWriter.WriteDeletedEventAsync(paymentOrder, null);
            }
        }
    }
}