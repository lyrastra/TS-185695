using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [OperationType(OperationType.PaymentOrderOutgoingTransferToAccount)]
    [InjectAsSingleton(typeof(ITransferToAccountRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class TransferToAccountRemover : ITransferToAccountRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly TransferToAccountApiClient transferToAccountApiClient;
        private readonly TransferFromAccountApiClient transferFromAccountApiClient;
        private readonly TransferToAccountEventWriter transferToAccountEventWriter;
        private readonly TransferFromAccountEventWriter transferFromAccountEventWriter;
        private readonly TransferToAccountLinksGetter linksGetter;

        public TransferToAccountRemover(
            IClosedPeriodValidator closedPeriodValidator,
            TransferToAccountApiClient transferToAccountApiClient,
            TransferFromAccountApiClient transferFromAccountApiClient,
            TransferToAccountEventWriter transferToAccountEventWriter,
            TransferFromAccountEventWriter transferFromAccountEventWriter,
            TransferToAccountLinksGetter linksGetter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.transferToAccountApiClient = transferToAccountApiClient;
            this.transferFromAccountApiClient = transferFromAccountApiClient;
            this.transferToAccountEventWriter = transferToAccountEventWriter;
            this.transferFromAccountEventWriter = transferFromAccountEventWriter;
            this.linksGetter = linksGetter;
        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var paymentOrder = await transferToAccountApiClient.GetAsync(documentBaseId);
            var transferFromAccountLink = await GetLinkedPaymentAsync(documentBaseId);

            if (transferFromAccountLink != null)
            {
                await closedPeriodValidator.ValidateAsync(transferFromAccountLink.Date);
            }
            await closedPeriodValidator.ValidateAsync(paymentOrder.Date);

            await DeleteLinkedPaymentAsync(transferFromAccountLink?.DocumentBaseId);
            await DeletePaymentAsync(documentBaseId, newDocumentBaseId);
        }

        private async Task<PaymentOrderLink> GetLinkedPaymentAsync(long documentBaseId)
        {
            var links = await linksGetter.GetAsync(documentBaseId);
            return links.TransferFromAccount.GetOrThrow();
        }

        private async Task DeletePaymentAsync(long documentBaseId, long? newDocumentBaseId)
        {
            var paymentOrder = await transferToAccountApiClient.GetAsync(documentBaseId);
            await transferToAccountApiClient.DeleteAsync(documentBaseId);
            await transferToAccountEventWriter.WriteDeletedEventAsync(paymentOrder, newDocumentBaseId);
        }

        private async Task DeleteLinkedPaymentAsync(long? linkedPaymentBaseId)
        {
            if (linkedPaymentBaseId.HasValue)
            {
                var paymentOrder = await transferFromAccountApiClient.GetAsync(linkedPaymentBaseId.Value);
                await transferFromAccountApiClient.DeleteAsync(paymentOrder.DocumentBaseId);
                await transferFromAccountEventWriter.WriteDeletedEventAsync(paymentOrder, null);
            }
        }
    }
}